using AutoMapper;
using GymManagmentSystem.BLL.Common;
using GymManagmentSystem.BLL.Services.Members.Interfaces;
using GymManagmentSystem.BLL.Services.Members.ViewModels;
using GymManagmentSystem.DAL.Models;
using GymManagmentSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem.BLL.Services.Members.classes;

public class MemberService : IMemberService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper mapper;

    public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result> CreateMemberAsync(CreateMemberViewModel model)
    {
        try
        {
            var members = await _unitOfWork.GetRepository<Member>().GetAllAsync();

            bool IsNotUniqePhone = members.Any(x => x.Phone == model.Phone);
            bool IsNotUniqeEmail = members.Any(x => x.Email == model.Email);

            if (IsNotUniqePhone)
                return Result.Fail("Phone already exists");

            if (IsNotUniqeEmail)
                return Result.Fail("Email already exists");

            var member = mapper.Map<Member>(model);

            await _unitOfWork.GetRepository<Member>().AddAsync(member);

            var result = await _unitOfWork.SaveChangesAsync() > 0;

            if (result)
            {
                return Result.Ok();
            }
            return Result.Fail("Failed to add member");
        }
        catch
        {
            return Result.Fail("Internal server error");
        }
    }
    public async Task<Result<IEnumerable<MemberViewModel>>> GetMembersAsync()
    {
        var members = await _unitOfWork.GetRepository<Member>().GetAllAsync();


        return Result<IEnumerable<MemberViewModel>>.Ok(mapper.Map<IEnumerable<MemberViewModel>>(members));

    }
    public async Task<Result<MemberDetailsViewModel?>> GetMemberAsync(int? Id)
    {
        if (Id == null || Id.Value == 0)
            return Result<MemberDetailsViewModel?>.Fail("Id not found");

        var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(Id.Value);


        if (member == null)
            return Result<MemberDetailsViewModel?>.Fail("Member not found");
        

        var model = mapper.Map<MemberDetailsViewModel>(member);


        var memberShips = await _unitOfWork.GetRepository<MemberShip>().GetAllAsync();
        var memberShip = memberShips.Where(x => x.MemberId == member.Id).ToList();

        if (memberShips.Any())
        {
            var membershipActive = memberShips.Where(x => x.IsActive).FirstOrDefault();
            if (membershipActive != null)
            {
                model.MembershipStartDate = membershipActive.CreatedAt.ToString("d/M/yyyy");
                model.MembershipEndDate = membershipActive.EndDate.ToString("d/M/yyyy");


                var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(membershipActive.PlanId);
                if (plan != null)
                {
                    model.PlanName = plan.Name;
                }
            }
        }
        return Result<MemberDetailsViewModel?>.Ok(model);

    }
    public async Task<Result<HealthRecordViewModel?>> GetHealthRecordDetailsAsync(int? Id)
    {
        var healthRecords = await _unitOfWork.GetRepository<HealthRecord>().GetAllAsync();
        if (healthRecords.Any())
        {
            var healthRecord = healthRecords.Where(x => x.MemberId == Id).FirstOrDefault();

            if (healthRecord != null)
            {
                var model = mapper.Map<HealthRecordViewModel>(healthRecord);
                return Result<HealthRecordViewModel?>.Ok(model);
            }
        }
        return Result<HealthRecordViewModel?>.Fail("Health record not found");

    }
    public async Task<Result<MemberToUpdateViewModel?>> GetMemberToUpdateAsync(int? Id)
    {
        if (Id == null || Id.Value == 0)
            return Result<MemberToUpdateViewModel?>.Fail("Id not found");

        var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(Id.Value);

        if (member == null)
            return Result<MemberToUpdateViewModel?>.Fail("Member not found");

        var model = mapper.Map<MemberToUpdateViewModel>(member);
        return Result<MemberToUpdateViewModel?>.Ok(model);
    }
    public async Task<Result> UpdateMemberAsync(MemberToUpdateViewModel model)
    {
        try
        {
            if (model == null)
                return Result.Fail("Model not found");

            var members = await _unitOfWork.GetRepository<Member>().GetAllAsync();
            bool IsNotUniqePhone = members.Any(x => x.Phone == model.Phone && x.Id != model.Id);
            bool IsNotUniqeEmail = members.Any(x => x.Email == model.Email && x.Id != model.Id);

            if (IsNotUniqePhone )
                return Result.Fail("Phone number is already exist");

            if(IsNotUniqeEmail)
                return Result.Fail("Email is already exist");


            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(model.Id);
            if (member == null)
                return Result.Fail("Member not found");

           var mappedMember= mapper.Map(model, member) ;

            await _unitOfWork.GetRepository<Member>().UpdateAsync(mappedMember) ;

            var result = await _unitOfWork.SaveChangesAsync() > 0;
           if (result)
           {
                return Result.Ok();
           }
            else
            {
              return Result.Fail("Failed to update member");
            }
        }
        catch
        {
            return Result.Fail("Internal server error");

        }
    }
    public async Task<Result> DeleteMemberAsync(int? Id)
    {
        try
        {
            if (Id == null || Id.Value == 0)
                return Result.Fail("Id not found");

            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(Id.Value);

            if (member == null)
                return Result.Fail("Member not found");

            await _unitOfWork.GetRepository<Member>().DeleteAsync(member);

            var result = await _unitOfWork.SaveChangesAsync() > 0;

            if (result)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Failed to delete member");
            }
        }
        catch
        {
            return Result.Fail("Internal server error");
        }

    }
}