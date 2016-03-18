namespace CommInfo.Migrations
{
    using CommInfo.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CommInfo.Models.CommInfoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "CommInfo.Models.CommInfoContext";
        }

        protected override void Seed(CommInfo.Models.CommInfoContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            /* For testing */
            // runs a debugger in a seperate instance of VisualStudio
            if (System.Diagnostics.Debugger.IsAttached == false)
                System.Diagnostics.Debugger.Launch();
            

            
            // Add an admin user
            // COMMENT OUT THIS (EXCEPT THE USERMANAGER), THE ROLE AND ADD ROLE ONCE THIS IS DONE
            UserManager<Member> userManager = new UserManager<Member>(new UserStore<Member>(context));
            /* */
            var user = new Member { UserName = "admin", NameFirst = "Admin", NameLast = "Istrator", Email = "test@test.com" };
            var result = userManager.Create(user, "testing");


            // Add a role
            context.Roles.Add(new IdentityRole() { Name = "Admin" });
            context.SaveChanges();  // saving here causes a "Validation failed for one or more entities." error

            // Add role to a user
            userManager.AddToRole(user.Id, "Admin");
             

            // From ForumDbInitilizer
            DateTime seedDate = new DateTime(2016, 01, 01);

            // Create Members
            // COMMENT OUT ONCE THE MEMEBERS ARE CREATED
            Member usr01 = new Member { UserName = "John", NameFirst = "John", NameLast = "Watson", Email = "jwatson@test.com" };
            var rusr01 = userManager.Create(usr01, "testing");
            Member usr02 = new Member { UserName = "David", NameFirst = "David", NameLast = "Yamamoto", Email = "bigwavedave@test.com" };
            var rusr02 = userManager.Create(usr02, "testing");
            Member usr03 = new Member { UserName = "Carrie", NameFirst = "Carrie", NameLast = "Tam", Email = "carrietam@test.com" };
            var rusr03 = userManager.Create(usr03, "testing");
            Member usr04 = new Member { UserName = "Nani", NameFirst = "Nani", NameLast = "Kealoha", Email = "nanik@test.com" };
            var rusr04 = userManager.Create(usr04, "testing");

            // once users are created, use the following:
            //var usr01 = context.Users.Where(u => u.UserName == "John").FirstOrDefault();
            //var usr02 = context.Users.Where(u => u.UserName == "David").FirstOrDefault();
            //var usr03 = context.Users.Where(u => u.UserName == "Carrie").FirstOrDefault();
            //var usr04 = context.Users.Where(u => u.UserName == "Nani").FirstOrDefault();


            // Create Message, Thread, and Fora(?) entities (based on Brian's example)
            // creates error: Object reference not set to an instance of an object
            Message msg01 = new Message { From = usr01.UserName, MemberID = usr01.Id, Date = seedDate, Subject = "Did you see the game?", Body = "The Alii trashed the Bulldogs!" };
            Message msg02 = new Message { From = usr02.UserName, MemberID = usr02.Id, Date = seedDate, Subject = "Meeting at Aiea Library this Friday", Body = "We're planning our next surf meet. Anyone want to come?" };
            Message msg03 = new Message { From = usr03.UserName, MemberID = usr03.Id, Date = seedDate, Subject = "Bookclub meeting this week?", Body = "Are we having a meeting this Thursday?" };
            Message msg04 = new Message { From = usr04.UserName, MemberID = usr04.Id, Date = seedDate, Subject = "Pearl Harbor Park Picnic", Body = "Bring a kite and the keiki! We have the musubi ready." };
            //Message msg01 = new Message { From = "John", Date = seedDate, Subject = "Did you see the game?", Body = "The Alii trashed the Bulldogs!" };
            //Message msg02 = new Message { From = "David", Date = seedDate, Subject = "Meeting at Aiea Library this Friday", Body = "We're planning our next surf meet. Anyone want to come?" };
            //Message msg03 = new Message { From = "Carrie", Date = seedDate, Subject = "Bookclub meeting this week?", Body = "Are we having a meeting this Thursday?" };
            //Message msg04 = new Message { From = "Nani", Date = seedDate, Subject = "Pearl Harbor Park Picnic", Body = "Bring a kite and the keiki! We have the musubi ready." };

            Thread thd01 = new Thread { Topic = "Aiea Vs. Farrington", Messages = { msg01 } };
            Thread thd02 = new Thread { Topic = "Surf Hui Planing Meeting", Messages = { msg02 } };
            Thread thd03 = new Thread { Topic = "Aiea Book Club", Messages = { msg03 } };
            Thread thd04 = new Thread { Topic = "Kite Club", Messages = { msg04 } };

            Forum frm01 = new Forum { ForumName = "Forum 1", Threads = { thd01, thd02, thd03, thd04 }, Messages = { msg01, msg02, msg03, msg04} }; // not sure
            //Forum frm01 = new Forum { ForumName = "Forum 1", Threads = { thd01, thd02, thd03, thd04 } };
            //Forum frm01 = new Forum { ForumName = "Community Forum", Threads = { thd01, thd02, thd03, thd04 } };

            //context.Users.AddOrUpdate(u => u.UserName, usr01, usr02, usr03, usr04);
            //context.Messages.AddOrUpdate(m => m.Subject, msg01, msg02, msg03, msg04);
            //context.Threads.AddOrUpdate(t => t.Topic, thd01, thd02, thd03, thd04);
            context.Fora.AddOrUpdate(f => f.ForumName, frm01);
            context.SaveChanges();

        }
    }
}
