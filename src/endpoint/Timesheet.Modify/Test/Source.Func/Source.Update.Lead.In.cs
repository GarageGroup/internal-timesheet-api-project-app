﻿using GarageGroup.Infra;
using Xunit;

namespace GarageGroup.Internal.Timesheet.Endpoint.Timesheet.Modify.Test;

internal static partial class TimesheetModifyFuncSource
{
    public static TheoryData<TimesheetUpdateIn, DataverseEntityGetOut<LeadJson>, DataverseEntityUpdateIn<TimesheetJson>> InputUpdateLeadTestData
        =>
        new()
        {
            {
                new(
                    timesheetId: new("555685cd-bdfb-42a5-aee6-8ad7f9c3b635"),
                    date: new(2021, 10, 07),
                    project: new(
                        id: new("7583b4e6-23f5-eb11-94ef-00224884a588"),
                        type: ProjectType.Lead),
                    duration: 8,
                    description: "Some message!"),
                new(
                    new()
                    {
                        Id = new("7583b4e6-23f5-eb11-94ef-00224884a588"),
                        CompanyName = "Some company name",
                        Subject = "Some subject"
                    }),
                new(
                    entityPluralName: "gg_timesheetactivities",
                    entityKey: new DataversePrimaryKey(new("555685cd-bdfb-42a5-aee6-8ad7f9c3b635")),
                    entityData: new()
                    {
                        Date = new(2021, 10, 07),
                        Description = "Some message!",
                        Duration = 8,
                        LeadLookupValue = "/leads(7583b4e6-23f5-eb11-94ef-00224884a588)",
                        Subject = "Some subject (Some company name)",
                    })
            },
            {
                new(
                    timesheetId: new("555685cd-bdfb-42a5-aee6-8ad7f9c3b635"),
                    date: null,
                    project: new(
                        id: new("7583b4e6-23f5-eb11-94ef-00224884a588"),
                        type: ProjectType.Lead),
                    duration: 8,
                    description: "Some message!"),
                new(
                    new()
                    {
                        Id = new("7583b4e6-23f5-eb11-94ef-00224884a588"),
                        CompanyName = null,
                        Subject = "Some subject"
                    }),
                new(
                    entityPluralName: "gg_timesheetactivities",
                    entityKey: new DataversePrimaryKey(new("555685cd-bdfb-42a5-aee6-8ad7f9c3b635")),
                    entityData: new()
                    {
                        Date = null,
                        Description = "Some message!",
                        Duration = 8,
                        LeadLookupValue = "/leads(7583b4e6-23f5-eb11-94ef-00224884a588)",
                        Subject = "Some subject"
                    })
            },
            {
                new(
                    timesheetId: new("555685cd-bdfb-42a5-aee6-8ad7f9c3b635"),
                    date: new(2021, 10, 07),
                    project: new(
                        id: new("7583b4e6-23f5-eb11-94ef-00224884a588"),
                        type: ProjectType.Lead),
                    duration: 8,
                    description: null),
                new(
                    new()
                    {
                        Id = new("7583b4e6-23f5-eb11-94ef-00224884a588"),
                        CompanyName = string.Empty,
                        Subject = "Some subject"
                    }),
                new(
                    entityPluralName: "gg_timesheetactivities",
                    entityKey: new DataversePrimaryKey(new("555685cd-bdfb-42a5-aee6-8ad7f9c3b635")),
                    entityData: new()
                    {
                        Date = new(2021, 10, 07),
                        Description = null,
                        Duration = 8,
                        LeadLookupValue = "/leads(7583b4e6-23f5-eb11-94ef-00224884a588)",
                        Subject = "Some subject",
                    })
            },
            {
                new(
                    timesheetId: new("555685cd-bdfb-42a5-aee6-8ad7f9c3b635"),
                    date: new(2021, 10, 07),
                    project: new(
                        id: new("7583b4e6-23f5-eb11-94ef-00224884a588"),
                        type: ProjectType.Lead),
                    duration: null,
                    description: "Some message!"),
                new(
                    new()
                    {
                        Id = new("7583b4e6-23f5-eb11-94ef-00224884a588"),
                        CompanyName = "Some company name",
                        Subject = "Some subject"
                    }),
                new(
                    entityPluralName: "gg_timesheetactivities",
                    entityKey: new DataversePrimaryKey(new("555685cd-bdfb-42a5-aee6-8ad7f9c3b635")),
                    entityData: new()
                    {
                        Date = new(2021, 10, 07),
                        Description = "Some message!",
                        Duration = null,
                        LeadLookupValue = "/leads(7583b4e6-23f5-eb11-94ef-00224884a588)",
                        Subject = "Some subject (Some company name)"
                    })
            },
            {
                new(
                    timesheetId: new("555685cd-bdfb-42a5-aee6-8ad7f9c3b635"),
                    date: null,
                    project: new(
                        id: new("7583b4e6-23f5-eb11-94ef-00224884a588"),
                        type: ProjectType.Lead),
                    duration: null,
                    description: null),
                new(
                    new()
                    {
                        Id = new("7583b4e6-23f5-eb11-94ef-00224884a588"),
                        CompanyName = "Some company name",
                        Subject = null
                    }),
                new(
                    entityPluralName: "gg_timesheetactivities",
                    entityKey: new DataversePrimaryKey(new("555685cd-bdfb-42a5-aee6-8ad7f9c3b635")),
                    entityData: new()
                    {
                        Date = null,
                        Description = null,
                        Duration = null,
                        LeadLookupValue = "/leads(7583b4e6-23f5-eb11-94ef-00224884a588)",
                        Subject = "(Some company name)"
                    })
            },
            {
                new(
                    timesheetId: new("555685cd-bdfb-42a5-aee6-8ad7f9c3b635"),
                    date: new(2021, 10, 07),
                    project: null,
                    duration: 8,
                    description: "Some message!"),
                new(default),
                new(
                    entityPluralName: "gg_timesheetactivities",
                    entityKey: new DataversePrimaryKey(new("555685cd-bdfb-42a5-aee6-8ad7f9c3b635")),
                    entityData: new()
                    {
                        Date = new(2021, 10, 07),
                        Description = "Some message!",
                        Duration = 8
                    })
            },
        };
}