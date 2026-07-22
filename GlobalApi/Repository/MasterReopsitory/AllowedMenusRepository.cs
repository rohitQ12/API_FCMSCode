using GlobalApi.Data;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.AdminClaims;
using GlobalApi.Repository.AdminRepository;
using Microsoft.EntityFrameworkCore;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class AllowedMenusRepository: IAllowedMenusRepository
    {
        private readonly GlobalContext db;
        public AllowedMenusRepository()
        {
            this.db = new GlobalContext();
        }
        public async Task<List<Menus_List>> Get(string roleId)
        {
            try
            {
                using(GlobalContext objEntity= new GlobalContext())
                {
                    var _result = (from a in objEntity.Menus
                                       //join b in db.SubMenu on a.M_Id equals b.SM_M_Id_FK 
                                   join c in objEntity.RoleClaims on a.M_Id equals c.RC_M_Id_FK
                                   where c.RC_RoleId_FK == roleId && a.Status == 1
                                   group new { a } by new { a.M_Id, a.M_label, a.M_icon, a.M_Title, a.M_Redirect_URL } into grouped
                                   select new Menus_List()
                                   {
                                       M_Id = grouped.Key.M_Id,
                                       M_label = grouped.Key.M_label,
                                       M_icon = grouped.Key.M_icon,
                                       link = grouped.Key.M_Redirect_URL,
                                       subItems = ((from d in objEntity.SubMenu
                                                    join j in objEntity.RoleClaims on d.SM_Id equals j.RC_SM_Id_FK
                                                    //where d.SM_M_Id_FK == grouped.Key.M_Id && d.SM_Id==c.
                                                    where d.SM_M_Id_FK == grouped.Key.M_Id && j.RC_RoleId_FK == roleId
                                                    group new { d } by new { d.SM_Id, d.SM_label, d.SM_icon, d.SM_link } into subgrp
                                                    select new SubMenu_List
                                                    {
                                                        SM_Id = subgrp.Key.SM_Id,
                                                        SM_label = subgrp.Key.SM_label,
                                                        SM_icon = subgrp.Key.SM_icon,
                                                        SM_link = subgrp.Key.SM_link,
                                                        subItemsList = ((from g in objEntity.SubMenu
                                                                         join h in objEntity.SubMenusFunctions on g.SM_Id equals h.SMF_SM_Id_FK
                                                                         join i in objEntity.SubRoleClaims on h.SMF_Id equals i.SRC_SMF_Id_FK
                                                                         where i.SRC_RoleId_FK == roleId
                                                                         group new { h } by new { h.SMF_Id, h.SMF_label, h.SMF_icon, h.SMF_link } into subgroup
                                                                         select new SubMenuFunctions_List
                                                                         {
                                                                             SMF_Id = subgroup.Key.SMF_Id,
                                                                             SMF_label = subgroup.Key.SMF_label,
                                                                             SMF_icon = subgroup.Key.SMF_icon,
                                                                             SMF_link = subgroup.Key.SMF_link,
                                                                         }).ToList())
                                                    }).ToList())
                                   }).ToListAsync();
                    return await _result;
                }
                

                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<ClaimsModels>> GetClims(int submenuid, string roleId)
        {
            try
            {

                var result=(from e in db.SubMenusDetails
                            join f in db.SubMenu on e.SMD_SM_Id_FK equals f.SM_Id
                            where e.SMD_SM_Id_FK== submenuid
                            select new ClaimsModels()
                            {
                                ClaimTypeId = e.SMD_Id,
                                IsClaimShown = e.SMD_IsClaimShown_In_UI,
                                ClaimType = e.SMD_SubMenusFunction,
                                ClaimValue = ((from g in db.SubMenusDetails
                                               join h in db.RoleClaims on e.SMD_Id equals h.RC_SMD_Id_FK
                                               where g.SMD_SM_Id_FK == f.SM_Id && h.RC_Value == "Y" && h.RC_RoleId_FK == roleId
                                               select g).Count()) >= 1 ? true : false
                            }).ToListAsync();

                return await result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
