using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VW.WebForm.Core.Model.Entities;
using Xrm.Tools.WebAPI;
using Xrm.Tools.WebAPI.Requests;

namespace GRomash.CrmWebApiEarlyBoundGenerator.TestApplication
{
    class Program
    {
        private static Guid _oppId = Guid.Parse("80360960-38BB-E811-8104-001DD8B71DC5");

        static async Task Main(string[] args)
        {
            var api = CrmWebApi();
            var items = await GetOpps(api);
            var first = items.First();


            var opportunity = new Opportunity()
            {
                Id = _oppId,
                ActualCloseDate = DateTime.Now,
                EstimatedValue = 12121,
                vw_BrandId = new EntityReference(vw_brand.EntitySetName, Guid.Parse("{28AA4355-3734-E911-8118-001DD8B71DC5}")),
                vw_YearTo = 2020,
                vw_IsUsedCar = true,
                vw_worklisttype = 4,
                Name = "Test web api"
            };

            var expandoObject = opportunity.ToExpandoObject();
            
            await api.Update(Opportunity.EntitySetName, _oppId, expandoObject);
        }


        private static CRMWebAPI CrmWebApi()
        {
            var crmWebApi = new CRMWebAPI("https://crm-vwapp.ncdev.ru/VWDevelop/api/data/v8.2/",
                new NetworkCredential("rgerasimov", "Vesnushka27"));
            return crmWebApi;
        }

        private static async Task<List<Opportunity>> GetOpps(CRMWebAPI crmWebApi)
        {
            var brandsItems = await crmWebApi.GetList(Opportunity.EntitySetName, new CRMGetListOptions()
            {
                Select = new[]
                {
                    Opportunity.Fields.vw_worklisttype,
                    Opportunity.Fields.vw_IsUsedCar,
                    Opportunity.Fields.CreatedOn,
                    Opportunity.Fields.BudgetAmount,
                    Opportunity.Fields.ActualCloseDate,
                    Opportunity.Fields.ActualValue,
                    Opportunity.Fields.ActualValue_Base,
                    Opportunity.Fields._vw_BrandId_value
                },
                Filter = $"{Opportunity.Fields.Id} eq {_oppId}",
                Expand = new[]
                {
                    new CRMExpandOptions()
                    {
                        Property = Opportunity.Properties.parentcontactid,
                    },
                    new CRMExpandOptions()
                    {
                        Property = Opportunity.Properties.vw_BrandId,
                        Select = new []
                        {
                            vw_brand.Fields.vw_brandId,
                            vw_brand.Fields.CreatedOn,
                        },
                    },
                }
            });

            var brands = brandsItems.List.ConvertAll(input => (Opportunity)Activator.CreateInstance(typeof(Opportunity), input));

            return brands;
        }

        private static async Task<List<vw_brand>> GetAsync(CRMWebAPI crmWebApi)
        {
            var brandsItems = await crmWebApi.GetList(vw_brand.EntitySetName, new CRMGetListOptions()
            {
                
            });

            var brands = brandsItems.List.ConvertAll(input => (vw_brand)Activator.CreateInstance(typeof(vw_brand), input));

            return brands;
        }
    }
}
