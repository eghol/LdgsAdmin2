using LdgsAdminAPI.Data;
using LdgsAdminAPI.DTO.db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LdgsAdminAPI
{
    public class hndlPermission
    {
        LdgsAdminDBContext _context;
        public hndlPermission(LdgsAdminDBContext context)
        {
            _context = context;
        }
        
        /* Function Take a List of Permissions given in plain text, check each Permission against DB and 
         * return the same info and add coresponding permissionID.         
        */
        public async Task<IList<dbPermissionType>> getPermissionDef(List<string> permissions = null)
        {
            if (permissions == null || permissions.Count==0) return null;

            List<dbPermissionType> lstPermissions = new();
            string missingPermissionDef = "";

            permissions.ForEach(async Permission =>
            {
                dbPermissionType pObj = await getPermissionByName(Permission);

                if (pObj != null)
                {                    
                    lstPermissions.Add(pObj);
                }
                else {                    
                    missingPermissionDef = missingPermissionDef +  Permission +", ";                
                }
            });

            return lstPermissions;

            //missingPermissionDef = "Permission: " + missingPermissionDef;
            
        }
       

        public async Task<dbPermissionType> getPermissionByName(string permissionName = "")
        {
            try
            {
                dbPermissionType permissionObj = await _context.PermissionTypes.Where(e => e.Name.ToLower() == permissionName.Trim().ToLower()).FirstOrDefaultAsync();
                if (permissionObj == null) return null;
                return permissionObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void addPermissions(List<dbPermissionType> Permissions)
        {

        }
        public void addPermission(string Permissions)
        {

        }

        private bool PermissionTypeExists(int id)
        {
            return _context.PermissionTypes.Any(e => e.Id == id);
        }


    }
}
