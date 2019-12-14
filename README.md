# crm.webApi.earlyBoundGenerator
Tool for generating entities model for Xrm.Tools.CRMWebAPI


#### Description

This XrmToolBox Plugin allows you to generate MS CRM entity models for future use with [Xrm.Tools.CRMWebAPI](https://github.com/davidyack/Xrm.Tools.CRMWebAPI/tree/master/dotnet "Xrm.Tools.CRMWebAPI")

[![Plugin](https://i.imgur.com/kYx80dl.jpg "Plugin")](https://i.imgur.com/kYx80dl.jpg "Plugin")

Generated models contain entity fields, properties for reading values, properties for expanding entities by lookup, and N:N relationships

#### Examples

Retrive entities

```csharp
            var brandsItems = await crmWebApi.GetList(Opportunity.EntitySetName, new CRMGetListOptions()
            {
                Select = new[]
                {
                    Opportunity.Fields.CreatedOn,
                    Opportunity.Fields.BudgetAmount,
                    Opportunity.Fields.ActualCloseDate,
                    Opportunity.Fields.ActualValue,
                    Opportunity.Fields.ActualValue_Base,
                },
                Filter = $"{Opportunity.Fields.Id} eq {_oppId}",
                Expand = new[]
                {
                    new CRMExpandOptions()
                    {
                        Property = Opportunity.Properties.parentcontactid,
                    },
                }
            });

            var opportunities = brandsItems.List.ConvertAll(input => (Opportunity)Activator.CreateInstance(typeof(Opportunity), input));
```

Update

```csharp
            var api = CrmWebApi();

            var opportunity = new Opportunity()
            {
                Id = _oppId,
                ActualCloseDate = DateTime.Now,
                EstimatedValue = 12121,
                ParentAccountId = new EntityReference("accounts", Guid.NewGuid()),
                StateCode = 1,
                StatusCode = 1,
                Name = "Name"
            };

            var expandoObject = opportunity.ToExpandoObject();
            
            await api.Update(Opportunity.EntitySetName, _oppId, expandoObject);
```


