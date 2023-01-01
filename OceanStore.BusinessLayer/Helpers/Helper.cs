using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Helpers
{
    public static class Helper
    {
        public enum Roles
        {
            SuperAdmin,
            Admin,
            Member
        }

        public static bool CheckActive(bool active)
        {
            return active ? false : true;
        }
    }
}
