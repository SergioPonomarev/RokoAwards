using RokoAwards.EFDataAccessLibrary.Models;
using RokoAwards.EFDataAccessLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace RokoAwards.EFDataAccessLibrary.DataAccess
{
    public static class SampleData
    {
        public static void Initialize(ApplicationContext context)
        {
            Image defaultUserImage = new Image
            {
                ImageName = "DefaultUserImage_DefaultUserImage.jpg",
                ImagePath = "/Images/UserImages/DefaultUserImage_DefaultUserImage.jpg"
            };

            Image defaultAwardImage = new Image
            {
                ImageName = "DefaultAwardImage_DefaultAwardImage.jpg",
                ImagePath = "/Images/AwardImages/DefaultAwardImage_DefaultAwardImage.jpg"
            };

            Image avaSergey = new Image
            {
                ImageName = "78312a8b-b16d-4560-8761-bc106e8de0c7_ava.jpg",
                ImagePath = "/Images/UserImages/78312a8b-b16d-4560-8761-bc106e8de0c7_ava.jpg"
            };

            Image thanksImage = new Image
            {
                ImageName = "ea3b7760-1511-4490-8d43-e3d3009178c5_A-Round-of-Thanks.jpg",
                ImagePath = "/Images/AwardImages/ea3b7760-1511-4490-8d43-e3d3009178c5_A-Round-of-Thanks.jpg"
            };

            Image gdAward = new Image
            {
                ImageName = "20680fd4-579e-44a1-befd-213981bf2fb3_greatdeveloper.jpg",
                ImagePath = "/Images/AwardImages/20680fd4-579e-44a1-befd-213981bf2fb3_greatdeveloper.jpg"
            };

            context.Images.Add(defaultUserImage);
            context.SaveChanges();
            context.Images.Add(defaultAwardImage);
            context.SaveChanges();
            context.Images.Add(avaSergey);
            context.SaveChanges();
            context.Images.Add(thanksImage);
            context.SaveChanges();
            context.Images.Add(gdAward);
            context.SaveChanges();

            Role adminRole = new Role { RoleName = "Admin" };
            Role userRole = new Role { RoleName = "User" };
            Role managerRole = new Role { RoleName = "Manager" };

            context.Roles.Add(adminRole);
            context.SaveChanges();
            context.Roles.Add(userRole);
            context.SaveChanges();
            context.Roles.Add(managerRole);
            context.SaveChanges();

            City saratov = new City { CityName = "Saratov" };
            context.Cities.Add(saratov);
            context.SaveChanges();

            Department dep1 = new Department { DepartmentName = ".NET back-end department" };
            Department dep2 = new Department { DepartmentName = ".NET front-end department" };
            Department dep3 = new Department { DepartmentName = "Administrative department" };
            Department dep4 = new Department { DepartmentName = "HR department" };
            Department dep5 = new Department { DepartmentName = "Client Support" };
            Department dep6 = new Department { DepartmentName = "DataBase department" };
            Department dep7 = new Department { DepartmentName = "Design department" };
            Department dep8 = new Department { DepartmentName = "DevOps department" };
            Department dep9 = new Department { DepartmentName = "Android department" };
            Department dep10 = new Department { DepartmentName = "iOS department" };
            Department dep11 = new Department { DepartmentName = "Management department" };
            Department dep12 = new Department { DepartmentName = "PHP department" };
            Department dep13 = new Department { DepartmentName = "Product Department" };
            Department dep14 = new Department { DepartmentName = "Quality Assurance Department" };
            Department dep15 = new Department { DepartmentName = "ROKO University" };

            context.Departments.Add(dep1);
            context.SaveChanges();
            context.Departments.Add(dep2);
            context.SaveChanges();
            context.Departments.Add(dep3);
            context.SaveChanges();
            context.Departments.Add(dep4);
            context.SaveChanges();
            context.Departments.Add(dep5);
            context.SaveChanges();
            context.Departments.Add(dep6);
            context.SaveChanges();
            context.Departments.Add(dep7);
            context.SaveChanges();
            context.Departments.Add(dep8);
            context.SaveChanges();
            context.Departments.Add(dep9);
            context.SaveChanges();
            context.Departments.Add(dep10);
            context.SaveChanges();
            context.Departments.Add(dep11);
            context.SaveChanges();
            context.Departments.Add(dep12);
            context.SaveChanges();
            context.Departments.Add(dep13);
            context.SaveChanges();
            context.Departments.Add(dep14);
            context.SaveChanges();
            context.Departments.Add(dep15);
            context.SaveChanges();

            User admin = new User
            {
                DateOfJoining = DateTime.Parse("07/17/2019"),
                DepartmentId = 1,
                FirstName = "Sergey",
                LastName = "Ponomarev",
                Email = "sergey.ponomarev@rokolabs.com",
                Status = Status.Active,
                PositionName = "Software Engineer SE2L1",
                ReportingManagerEmail = "aleksei.guzev@rokolabs.com",
                CityId = 1,
                ImageId = 3,
                HashedPassword = "0B3500B05D4908E0A0359B69394024C49B82C5F9297B1DD90839465A41AD115B",
                RoleId = 1
            };

            User testDummy = new User
            {
                DateOfJoining = DateTime.Parse("02/27/2020"),
                DepartmentId = 1,
                FirstName = "Test",
                LastName = "Dummy",
                Email = "test@test.com",
                Status = Status.Active,
                PositionName = "Junior Software Engineer SE1L1",
                ReportingManagerEmail = "sergey.chernov@rokolabs.com",
                CityId = 1,
                ImageId = 1,
                HashedPassword = "9646237294FA20C49A6141F21F63DE9A59AA0FAFD59968A967D7B69A3CD2469B",
                RoleId = 2
            };

            User rokoUniversity = new User
            {
                DateOfJoining = DateTime.Parse("01/01/2017"),
                DepartmentId = 15,
                FirstName = "ROKO",
                LastName = "University",
                Email = "rokouniversity.saratov@rokolabs.com",
                Status = Status.Special,
                PositionName = "Roko University",
                ReportingManagerEmail = "aleksandr.kuznetsov@rokolabs.com",
                CityId = 1,
                ImageId = 1,
                HashedPassword = "C414D3D88A0B799D264C28495DB17806A584E4F87AD99CEF1D61CA78B7AE79AF",
                RoleId = 3
            };

            User anotherTest = new User
            {
                DateOfJoining = DateTime.Parse("02/03/2020"),
                DepartmentId = 1,
                FirstName = "Another",
                LastName = "Test",
                Email = "another.test@test.com",
                Status = Status.Active,
                PositionName = "Junior Software Engineer SE1L1",
                ReportingManagerEmail = "sergey.chernov@rokolabs.com",
                CityId = 1,
                ImageId = 1,
                HashedPassword = "88B27608D3106A409D4E32F36125D25487A89C48C842AE1357F6C8D0449FC8AF",
                RoleId = 2
            };

            User anastasiaBaeva = new User
            {
                DateOfJoining = DateTime.Parse("08/12/2019"),
                DepartmentId = 14,
                FirstName = "Anastasia",
                LastName = "Baeva",
                Email = "anastasia.baeva@rokolabs.com",
                Status = Status.Active,
                PositionName = "Lead Software Testing Engineer QA4L3",
                ReportingManagerEmail = "ekaterina.kurbatova@rokolabs.com",
                CityId = 1,
                ImageId = 1,
                HashedPassword = "ECB82BDF2DCBD1514AB178CD51D77CAC64BF8784F073E9EFA9EAD8857EB014C3",
                RoleId = 3
            };

            context.Users.Add(admin);
            context.SaveChanges();
            context.Users.Add(testDummy);
            context.SaveChanges();
            context.Users.Add(rokoUniversity);
            context.SaveChanges();
            context.Users.Add(anotherTest);
            context.SaveChanges();
            context.Users.Add(anastasiaBaeva);
            context.SaveChanges();

            Award greatDeveloper = new Award
            {
                CreaterId = 1,
                CreatingDate = DateTime.Parse("02/28/2020"),
                AwardType = AwardType.Award,
                Description = "Award for very great developers.",
                ImageId = 5,
                AwardTitle = "Great developer"
            };

            Award awardWithLargeDescription = new Award
            {
                CreaterId = 1,
                CreatingDate = DateTime.Parse("03/03/2020"),
                AwardType = AwardType.Award,
                Description = "Here goes very large description of this award. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec quis dictum metus, sit amet semper neque. Nullam rutrum tortor placerat, auctor ligula quis, rhoncus nulla. Duis posuere, turpis at iaculis iaculis, erat erat interdum dui, non vehicula velit orci ut tellus. Suspendisse potenti. Sed vitae tempor dolor. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vivamus venenatis rutrum tellus, vel consequat velit porta ut. Nullam eget purus efficitur, sollicitudin erat maximus, fringilla sapien. Ut id dignissim ipsum. Sed sed tincidunt nibh. Donec vitae ligula a magna laoreet iaculis at eu nisl. In vulputate aliquet eros, at efficitur lorem blandit id. Sed maximus semper ex a venenatis.",
                ImageId = 2,
                AwardTitle = "Award with large description"
            };

            Award thanksAward = new Award
            {
                CreaterId = 1,
                CreatingDate = DateTime.Parse("03/03/2020"),
                AwardType = AwardType.Thanks,
                Description = "Thank you from a colleague.",
                ImageId = 2,
                AwardTitle = "Thanks"
            };

            context.Awards.Add(greatDeveloper);
            context.SaveChanges();
            context.Awards.Add(awardWithLargeDescription);
            context.SaveChanges();
            context.Awards.Add(thanksAward);
            context.SaveChanges();

            UserAward ua1 = new UserAward
            {
                UserId = 4,
                AwardIdReceived = 1,
                FromUserId = 1,
                AwardIdSent = 1,
                AwardDate = DateTime.Parse("03/03/2020"),
                Description = "Good boy."
            };

            UserAward ua2 = new UserAward
            {
                UserId = 4,
                AwardIdReceived = 1,
                FromUserId = 1,
                AwardIdSent = 1,
                AwardDate = DateTime.Parse("03/03/2020"),
                Description = "Good boy again."
            };

            UserAward ua3 = new UserAward
            {
                UserId = 4,
                AwardIdReceived = 2,
                FromUserId = 1,
                AwardIdSent = 2,
                AwardDate = DateTime.Parse("03/03/2020"),
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Suspendisse a facilisis nisi.In at semper ex.Mauris imperdiet finibus elit.Proin vitae ante bibendum, eleifend est eget, auctor lectus.Sed malesuada leo lectus, vitae auctor turpis consectetur in. Cras pulvinar metus varius nunc vestibulum, id consectetur velit blandit.Fusce et felis id nisl interdum accumsan nec et dolor.Ut lectus ligula, facilisis et massa a, euismod porta mauris.Ut non iaculis nibh.Donec pellentesque, massa vel condimentum finibus, eros quam condimentum ex, non pharetra velit eros ut urna.Mauris in tellus eleifend, finibus augue non, facilisis nisi.Aenean nec finibus nisl, a egestas lectus.Aliquam eget risus tincidunt, posuere risus non, egestas nisl.Aenean justo ligula, mollis nec sagittis ut, dapibus ut quam.Nullam tincidunt arcu eu massa lobortis, at sodales mauris feugiat.Aenean euismod risus metus, a imperdiet tortor aliquam eget."
            };

            UserAward ua4 = new UserAward
            {
                UserId = 4,
                AwardIdReceived = 3,
                FromUserId = 1,
                AwardIdSent = 3,
                AwardDate = DateTime.Parse("03/03/2020"),
                Description = "Thanks body. You are the best man in the world."
            };

            UserAward ua5 = new UserAward
            {
                UserId = 2,
                AwardIdReceived = 1,
                FromUserId = 1,
                AwardIdSent = 1,
                AwardDate = DateTime.Parse("03/03/2020"),
                Description = "Porque tu lo vales."
            };

            UserAward ua6 = new UserAward
            {
                UserId = 4,
                AwardIdReceived = 2,
                FromUserId = 1,
                AwardIdSent = 2,
                AwardDate = DateTime.Parse("03/03/2019"),
                Description = "Last year you was good."
            };

            UserAward ua7 = new UserAward
            {
                UserId = 4,
                AwardIdReceived = 1,
                FromUserId = 1,
                AwardIdSent = 1,
                AwardDate = DateTime.Parse("03/03/2019"),
                Description = "Last year you was very good."
            };

            UserAward ua8 = new UserAward
            {
                UserId = 4,
                AwardIdReceived = 3,
                FromUserId = 1,
                AwardIdSent = 3,
                AwardDate = DateTime.Parse("03/03/2019"),
                Description = "Thank you for last year."
            };

            UserAward ua9 = new UserAward
            {
                UserId = 1,
                AwardIdReceived = 3,
                FromUserId = 5,
                AwardIdSent = 3,
                AwardDate = DateTime.Parse("03/03/2019"),
                Description = "For good app."
            };

            context.UserAwards.Add(ua1);
            context.SaveChanges();
            context.UserAwards.Add(ua2);
            context.SaveChanges();
            context.UserAwards.Add(ua3);
            context.SaveChanges();
            context.UserAwards.Add(ua4);
            context.SaveChanges();
            context.UserAwards.Add(ua5);
            context.SaveChanges();
            context.UserAwards.Add(ua6);
            context.SaveChanges();
            context.UserAwards.Add(ua7);
            context.SaveChanges();
            context.UserAwards.Add(ua8);
            context.SaveChanges();
            context.UserAwards.Add(ua9);
            context.SaveChanges();
        }
    }
}
