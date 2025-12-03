using Abyat.Bl.Contracts;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos;
using Abyat.Bl.Dtos.Detailed;
using Abyat.Bl.Services.Base;
using Abyat.Domains.Contracts;
using Abyat.Domains.Contracts.Models;
using Abyat.Domains.Models;
using AutoMapper;
using static Abyat.Bl.Enums.ServicesEnums;
using static Abyat.Core.Enums.Status.Status;

namespace Abyat.Bl.Services;

public class ProjectService(
    ITableQryRepo<TbProject> repoQuery,
    ITableCmdRepo<TbProject> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    IProjectImage projectImage,
    ICrudPublisher publisher)
    : BaseService<TbProject, ProjectDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    IProject
{
    public async Task<(bool success, int id)> AddAsync(
        ProjectDto entity,
        IEnumerable<int> imageSizeIds,
        bool fireEvent = true)
    {
        var add = await base.AddAsync(entity, fireEvent);

        if (imageSizeIds.Any())
        {
            foreach (int id in imageSizeIds)
            {
                if (await projectImage.IsExistsAsync(p => p.ProjectId == add.id && p.ImageSizeId == id))
                    continue;

                await projectImage.AddAsync(new ProjectImageDto
                {
                    ProjectId = add.id,
                    ImageSizeId = id,
                });
            }
        }

        return (add.success, add.id);
    }

    public async Task<bool> HasImgs(int id) => await projectImage.IsExistsAsync(p => p.ProjectId == id);

    public async Task<ImageDto>? GetFirstMedImg(int id)
    {
        if (!await HasImgs(id))
            return null;

        var images = await projectImage.GetProjectImgsAsync(id);

        if (images is null)
            return null;
        else
            return images.FirstOrDefault().MediumSize;
    }

    public async Task<bool> UpdateAsync(
        ProjectDto entity,
        IEnumerable<int> imageSizeIds,
        bool fireEvent)
    {
        var update = await base.UpdateAsync(entity, fireEvent);

        if (imageSizeIds.Any())
        {
            foreach (int imgSizeId in imageSizeIds)
            {
                if (await projectImage.IsExistsAsync(p => p.ProjectId == entity.Id && p.ImageSizeId == imgSizeId))
                    continue;

                await projectImage.AddAsync(new ProjectImageDto
                {
                    ProjectId = entity.Id,
                    ImageSizeId = imgSizeId,
                });
            }
        }

        return update;
    }

    public async Task<List<ImageSizeDto>> GetImgsAsync(int Id)
    {
        return await projectImage.GetProjectImgsAsync(Id);
    }

    public async Task<PagedResult<ProjectDetailedDto>> GetPagedList(int pageNumber = 1, int pageSize = 50)
    {
        return await repoQuery.GetPagedListAsync(
            pageNumber,
            pageSize,
            p => p.CurrentState == enCurrentState.Active,
            p => new ProjectDetailedDto
            {
                Id = p.Id,
                TitleEn = p.TitleEn,
                TitleAr = p.TitleAr,
                DescriptionEn = p.DescriptionEn,
                DescriptionAr = p.DescriptionAr,
                Slug = p.Slug,
                Order = p.Order,
                ClientEn = p.Client.NameEn ?? string.Empty,
                ClientAr = p.Client.NameAr ?? string.Empty,
                ImageUrl = p.ProjectImages.FirstOrDefault() != null ? p.ProjectImages.FirstOrDefault()!.ImageSize.MediumSize.Url : string.Empty,
            },
            p => p.Order,
            false,
            default,
            p => p.Client, p => p.ProjectImages);
    }

}