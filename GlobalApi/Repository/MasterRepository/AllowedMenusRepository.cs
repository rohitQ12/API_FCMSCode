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
        GlobalContext globalcontext;
        public AllowedMenusRepository(GlobalContext globalcontext)
        {
            this.globalcontext = globalcontext;
        }
        public async Task<List<Menus_List>> Get(string roleId)
        {
            try
            {

                var _result = (from a in globalcontext.Menus
                                   //join b in globalcontext.SubMenu on a.M_Id equals b.SM_M_Id_FK 
                               join c in globalcontext.RoleClaims on a.M_Id equals c.RC_M_Id_FK
                               where c.RC_RoleId_FK == roleId
                               group new { a } by new { a.M_Id, a.M_label, a.M_icon, a.M_Title, a.M_Redirect_URL } into grouped
                               select new Menus_List()
                               {
                                   M_Id = grouped.Key.M_Id,
                                   M_label = grouped.Key.M_label,
                                   M_icon = grouped.Key.M_icon,
                                   link= grouped.Key.M_Redirect_URL,
                                   subItems = ((from d in globalcontext.SubMenu
                                                //join j in RoleClaims on d.SM_Id equals j.RC_SM_Id_FK
                                                //where d.SM_M_Id_FK == grouped.Key.M_Id && d.SM_Id==c.
                                                where d.SM_M_Id_FK == grouped.Key.M_Id
                                                group new { d } by new { d.SM_Id, d.SM_label, d.SM_icon, d.SM_link } into subgrp
                                                select new SubMenu_List
                                                {
                                                    SM_Id = subgrp.Key.SM_Id,
                                                    SM_label = subgrp.Key.SM_label,
                                                    SM_icon = subgrp.Key.SM_icon,
                                                    SM_link = subgrp.Key.SM_link,
                                                    subItemsList = ((from g in globalcontext.SubMenu
                                                                     join h in globalcontext.SubMenusFunctions on g.SM_Id equals h.SMF_SM_Id_FK
                                                                     join i in globalcontext.SubRoleClaims on h.SMF_Id equals i.SRC_SMF_Id_FK
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<ClaimsModels>> GetClims(int submenuid,string roleId)
        {
            try
            {
                var result=(from e in globalcontext.SubMenusDetails
                            join f in globalcontext.SubMenu on e.SMD_SM_Id_FK equals f.SM_Id
                            where e.SMD_SM_Id_FK == submenuid
                            select new ClaimsModels()
                            {
                                ClaimTypeId = e.SMD_Id,
                                IsClaimShown = e.SMD_IsClaimShown_In_UI,
                                ClaimType = e.SMD_SubMenusFunction,
                                ClaimValue = ((from g in globalcontext.SubMenusDetails
                                               join h in globalcontext.RoleClaims on e.SMD_Id equals h.RC_SMD_Id_FK
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
