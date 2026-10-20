using GymManagmentSystem.DAL.Models.Enums;

namespace GymManagmentSystem.DAL.Models
{
    public class Trainer : GymUser
    {
        // JoinDate => CreatedAt
        public Speciality Speciality { get; set; }

    }
}
