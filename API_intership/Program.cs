using API_intership.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_intership
{
    class Program
    {         
        static void Main(string[] args)
        {
            int user=0;
            Console.WriteLine("Internship exercise");
            ApiController api = new ApiController();
            api.ConfigureHttpClient();
            do
            {
                try
                {
                    user = Convert.ToInt32(Console.ReadLine());
                }catch(Exception e)
                {
                    user = 0;
                }
                switch (user)
                {
                    case 1:
                        api.AddNewUser();
                        break;
                    case 2:
                        api.GetCreatedUser();
                        break;
                    case 3:
                        api.GetSkills();
                        break;
                    case 4:
                        api.AddSkillToUser();
                        break;
                    case 5:
                        api.AddDetailsToUser();
                        break;
                    case 6:
                        api.GetAllUserDetails();
                        break;
                    case 7:
                        api.AddSkill();
                        break;
                }
            } while (user != 0);

        }
        

    }
}
