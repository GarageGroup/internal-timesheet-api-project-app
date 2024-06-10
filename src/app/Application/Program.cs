﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace GarageGroup.Internal.Timesheet;

static class Program
{
    static Task Main(string[] args)
        =>
        AzureApplication.Create(args)
        .UseHealthCheck()
        .UseSwagger()
        .UseStandardSwaggerUI()
        .UseJwtReader()
        .UseProjectSetSearchEndpoint()
        .UseLastProjectSetGetEndpoint()
        .UseTimesheetSetGetEndpoint()
        .UseTimesheetCreateEndpoint()
        .UseTimesheetDeleteEndpoint()
        .UseTimesheetUpdateEndpoint()
        .UseTagSetGetEndpoint()
	    .UseNotificationSubscribeEndpoint()
        .RunAsync();
}
