using AutoMapper;
using TaskManagerApi.Models;
using TaskManagerApi.DTOs.Users.Admin;
using TaskManagerApi.DTOs.Tasks;
using TaskManagerApi.DTOs.Tags;
using TaskManagerApi.DTOs.Categories;
using TaskManagerApi.DTOs.Comments;
using TaskManagerApi.DTOs.Users;

namespace TaskManagerApi.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ---------------------------------------------------------
        // USER MAPPINGS
        // ---------------------------------------------------------

        // Normal user read
        CreateMap<ApplicationUser, UserReadDto>();

        // Admin read (roles hämtas separat)
        CreateMap<ApplicationUser, UserAdminReadDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore());

        // List item
        CreateMap<ApplicationUser, UserListItemDto>();

        // UPDATE DTOs ska INTE mappas automatiskt
        // därför ingen CreateMap<UserUpdateDto, ApplicationUser>()
        // därför ingen CreateMap<UserAdminUpdateDto, ApplicationUser>()


        // ---------------------------------------------------------
        // CATEGORY MAPPINGS
        // ---------------------------------------------------------

        CreateMap<Category, CategoryReadDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();


        // ---------------------------------------------------------
        // TAG MAPPINGS
        // ---------------------------------------------------------

        CreateMap<Tag, TagReadDto>();
        CreateMap<TagCreateDto, Tag>();
        CreateMap<TagUpdateDto, Tag>();

        // Tag inside TaskReadDto
        CreateMap<Tag, TagDto>();


        // ---------------------------------------------------------
        // COMMENT MAPPINGS
        // ---------------------------------------------------------

        // För endpoints som returnerar kommentarer separat
        CreateMap<Comment, CommentReadDto>()
         .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
         .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
         .ForMember(dest => dest.IsOwner, opt => opt.Ignore());


        // För kommentarer inuti TaskReadDto
        CreateMap<Comment, TaskCommentDto>()
            .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName));


        // ---------------------------------------------------------
        // TASK MAPPINGS
        // ---------------------------------------------------------

        // LIST VIEW (TaskListItemDto)
        CreateMap<TaskItem, TaskListItemDto>()
        .ForMember(dest => dest.CategoryName,
            opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
        .ForMember(dest => dest.Tags,
            opt => opt.MapFrom(src => src.TaskTags.Select(tt => tt.Tag.Name)))
        .ForMember(dest => dest.Deadline,
            opt => opt.MapFrom(src => src.Deadline));



        // DETAIL VIEW (TaskReadDto)
        CreateMap<TaskItem, TaskReadDto>()
       .ForMember(dest => dest.CategoryName,
           opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
       .ForMember(dest => dest.Tags,
           opt => opt.MapFrom(src => src.TaskTags.Select(tt => tt.Tag)))
       .ForMember(dest => dest.Comments,
           opt => opt.MapFrom(src => src.Comments))
       .ForMember(dest => dest.Deadline,
           opt => opt.MapFrom(src => src.Deadline));


        // CREATE
        CreateMap<TaskCreateDto, TaskItem>();

        // UPDATE (OBS: null-värden ska inte skrivas över)
        CreateMap<TaskUpdateDto, TaskItem>()
            .ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));
    }
}
