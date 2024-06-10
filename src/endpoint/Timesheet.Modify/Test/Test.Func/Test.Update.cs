﻿using System;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;
using Moq;
using Xunit;

namespace GarageGroup.Internal.Timesheet.Endpoint.Timesheet.Modify.Test;

partial class TimesheetModifyFuncTest
{
    [Fact]
    internal static async Task UpdateInvokeAsync_InputProjectTypeIsProject_ExpectDataverseGetCalledOnce()
    {
        var mockDataverseApi = BuildMockDataverseApi<ProjectJson>(Result.Success<Unit>(default), SomeProjectJsonOut);
        var func = new TimesheetModifyFunc(mockDataverseApi.Object);

        var cancellationToken = new CancellationToken(false);
        var input = new TimesheetUpdateIn(
            timesheetId: Guid.Parse("80108b86-61ae-47ea-bd61-6d0c126a42b4"),
            date: new(2024, 06, 07),
            project: new TimesheetProject(
                id: new("190fd90c-64be-4d6e-8764-44c567b40ef9"),
                type: ProjectType.Project),
            duration: 2,
            description: "Some description");

        _ = await func.InvokeAsync(input, cancellationToken);

        var expectInput = new DataverseEntityGetIn(
            entityPluralName: "gg_projects",
            entityKey: new DataversePrimaryKey(new("190fd90c-64be-4d6e-8764-44c567b40ef9")),
            selectFields: ["gg_name"]);

        mockDataverseApi.Verify(a => a.GetEntityAsync<ProjectJson>(expectInput, cancellationToken), Times.Once);
    }

    [Fact]
    internal static async Task UpdateInvokeAsync_InputProjectTypeIsIncident_ExpectDataverseGetCalledOnce()
    {
        var mockDataverseApi = BuildMockDataverseApi<IncidentJson>(Result.Success<Unit>(default), SomeIncidentJsonOut);
        var func = new TimesheetModifyFunc(mockDataverseApi.Object);

        var cancellationToken = new CancellationToken(false);
        var input = new TimesheetUpdateIn(
            timesheetId: Guid.Parse("80108b86-61ae-47ea-bd61-6d0c126a42b4"),
            date: new(2024, 06, 07),
            project: new TimesheetProject(
                id: new("190fd90c-64be-4d6e-8764-44c567b40ef9"),
                type: ProjectType.Incident),
            duration: 2,
            description: "Some description");

        _ = await func.InvokeAsync(input, cancellationToken);

        var expectInput = new DataverseEntityGetIn(
            entityPluralName: "incidents",
            entityKey: new DataversePrimaryKey(new("190fd90c-64be-4d6e-8764-44c567b40ef9")),
            selectFields: ["title"]);

        mockDataverseApi.Verify(a => a.GetEntityAsync<IncidentJson>(expectInput, cancellationToken), Times.Once);
    }

    [Fact]
    internal static async Task UpdateInvokeAsync_InputProjectTypeIsOpportunity_ExpectDataverseGetCalledOnce()
    {
        var mockDataverseApi = BuildMockDataverseApi<OpportunityJson>(Result.Success<Unit>(default), SomeOpportunityJsonOut);
        var func = new TimesheetModifyFunc(mockDataverseApi.Object);

        var cancellationToken = new CancellationToken(false);
        var input = new TimesheetUpdateIn(
            timesheetId: Guid.Parse("80108b86-61ae-47ea-bd61-6d0c126a42b4"),
            date: new(2024, 06, 07),
            project: new TimesheetProject(
                id: new("190fd90c-64be-4d6e-8764-44c567b40ef9"),
                type: ProjectType.Opportunity),
            duration: 2,
            description: "Some description");

        _ = await func.InvokeAsync(input, cancellationToken);

        var expectInput = new DataverseEntityGetIn(
            entityPluralName: "opportunities",
            entityKey: new DataversePrimaryKey(new("190fd90c-64be-4d6e-8764-44c567b40ef9")),
            selectFields: ["name"]);

        mockDataverseApi.Verify(a => a.GetEntityAsync<OpportunityJson>(expectInput, cancellationToken), Times.Once);
    }

    [Fact]
    internal static async Task UpdateInvokeAsync_InputProjectTypeIsLead_ExpectDataverseGetCalledOnce()
    {
        var mockDataverseApi = BuildMockDataverseApi<LeadJson>(Result.Success<Unit>(default), SomeLeadJsonOut);
        var func = new TimesheetModifyFunc(mockDataverseApi.Object);

        var cancellationToken = new CancellationToken(false);
        var input = new TimesheetUpdateIn(
            timesheetId: Guid.Parse("80108b86-61ae-47ea-bd61-6d0c126a42b4"),
            date: new(2024, 06, 07),
            project: new TimesheetProject(
                id: new("190fd90c-64be-4d6e-8764-44c567b40ef9"),
                type: ProjectType.Lead),
            duration: 2,
            description: "Some description");

        _ = await func.InvokeAsync(input, cancellationToken);

        var expectInput = new DataverseEntityGetIn(
            entityPluralName: "leads",
            entityKey: new DataversePrimaryKey(new("190fd90c-64be-4d6e-8764-44c567b40ef9")),
            selectFields: ["companyname", "subject"]);

        mockDataverseApi.Verify(a => a.GetEntityAsync<LeadJson>(expectInput, cancellationToken), Times.Once);
    }

    [Fact]
    internal static async Task UpdateInvokeAsync_InputProjectTypeIsInvalid_ExpectUnexpectedProjectTypeFailureCode()
    {
        var input = new TimesheetUpdateIn(
            timesheetId: Guid.Parse("80108b86-61ae-47ea-bd61-6d0c126a42b4"),
            date: new(2024, 06, 07),
            project: new TimesheetProject(
                id: new("190fd90c-64be-4d6e-8764-44c567b40ef9"),
                type: (ProjectType)(-5)),
            duration: 2,
            description: "Some description");

        var mockDataverseApi = BuildMockDataverseApi<OpportunityJson>(Result.Success<Unit>(default), SomeOpportunityJsonOut);

        var func = new TimesheetModifyFunc(mockDataverseApi.Object);

        var actual = await func.InvokeAsync(input, default);
        var expected = Failure.Create(TimesheetUpdateFailureCode.UnexpectedProjectType, "An unexpected project type: -5");

        Assert.StrictEqual(expected, actual);
    }

    [Fact]
    internal static async Task UpdateInvokeAsync_InputIsValidProjectIdIsInvalid_ExpectProjectNotFoundFailureCode()
    {
        var sourceException = new Exception("Some exception message");
        var dataverseFailure = sourceException.ToFailure(DataverseFailureCode.RecordNotFound, "Some failure text");
        var mockDataverseApi = BuildMockDataverseApi<ProjectJson>(Result.Success<Unit>(default), dataverseFailure);

        var func = new TimesheetModifyFunc(mockDataverseApi.Object);

        var input = new TimesheetUpdateIn(
            timesheetId: Guid.Parse("80108b86-61ae-47ea-bd61-6d0c126a42b4"),
            date: new(2024, 06, 07),
            project: new TimesheetProject(
                id: new("190fd90c-64be-4d6e-8764-44c567b40ef9"),
                type: ProjectType.Project),
            duration: 2,
            description: "Some description");

        var actual = await func.InvokeAsync(input, default);
        var expected = Failure.Create(TimesheetUpdateFailureCode.ProjectNotFound, "Some failure text", sourceException);

        Assert.StrictEqual(expected, actual);
    }

    [Theory]
    [MemberData(nameof(TimesheetModifyFuncSource.InputUpdateProjectTestData), MemberType = typeof(TimesheetModifyFuncSource))]
    internal static async Task UpdateInvokeAsync_InputIsValidNullForProject_ExpectDataverseUpdateCalledOnce(
        TimesheetUpdateIn input, DataverseEntityGetOut<ProjectJson> dataverseOut, DataverseEntityUpdateIn<TimesheetJson> expectedInput)
    {
        var mockDataverseApi = BuildMockDataverseApi<ProjectJson>(Result.Success<Unit>(default), dataverseOut);
        var func = new TimesheetModifyFunc(mockDataverseApi.Object);

        var cancellationToken = new CancellationToken(false);
        _ = await func.InvokeAsync(input, cancellationToken);

        mockDataverseApi.Verify(a => a.UpdateEntityAsync(expectedInput, cancellationToken), Times.Once);
    }

    [Theory]
    [MemberData(nameof(TimesheetModifyFuncSource.InputUpdateIncidentTestData), MemberType = typeof(TimesheetModifyFuncSource))]
    internal static async Task UpdateInvokeAsync_InputIsValidNullForIncident_ExpectDataverseUpdateCalledOnce(
        TimesheetUpdateIn input, DataverseEntityGetOut<IncidentJson> dataverseOut, DataverseEntityUpdateIn<TimesheetJson> expectedInput)
    {
        var mockDataverseApi = BuildMockDataverseApi<IncidentJson>(Result.Success<Unit>(default), dataverseOut);
        var func = new TimesheetModifyFunc(mockDataverseApi.Object);

        var cancellationToken = new CancellationToken(false);
        _ = await func.InvokeAsync(input, cancellationToken);

        mockDataverseApi.Verify(a => a.UpdateEntityAsync(expectedInput, cancellationToken), Times.Once);
    }

    [Theory]
    [MemberData(nameof(TimesheetModifyFuncSource.InputUpdateOpportunityTestData), MemberType = typeof(TimesheetModifyFuncSource))]
    internal static async Task UpdateInvokeAsync_InputIsValidNullForOpportunity_ExpectDataverseUpdateCalledOnce(
        TimesheetUpdateIn input, DataverseEntityGetOut<OpportunityJson> dataverseOut, DataverseEntityUpdateIn<TimesheetJson> expectedInput)
    {
        var mockDataverseApi = BuildMockDataverseApi<OpportunityJson>(Result.Success<Unit>(default), dataverseOut);
        var func = new TimesheetModifyFunc(mockDataverseApi.Object);

        var cancellationToken = new CancellationToken(false);
        _ = await func.InvokeAsync(input, cancellationToken);

        mockDataverseApi.Verify(a => a.UpdateEntityAsync(expectedInput, cancellationToken), Times.Once);
    }

    [Theory]
    [MemberData(nameof(TimesheetModifyFuncSource.InputUpdateLeadTestData), MemberType = typeof(TimesheetModifyFuncSource))]
    internal static async Task UpdateInvokeAsync_InputIsValidNullForLead_ExpectDataverseUpdateCalledOnce(
        TimesheetUpdateIn input, DataverseEntityGetOut<LeadJson> dataverseOut, DataverseEntityUpdateIn<TimesheetJson> expectedInput)
    {
        var mockDataverseApi = BuildMockDataverseApi<LeadJson>(Result.Success<Unit>(default), dataverseOut);
        var func = new TimesheetModifyFunc(mockDataverseApi.Object);

        var cancellationToken = new CancellationToken(false);
        _ = await func.InvokeAsync(input, cancellationToken);

        mockDataverseApi.Verify(a => a.UpdateEntityAsync(expectedInput, cancellationToken), Times.Once);
    }

    [Theory]
    [InlineData(DataverseFailureCode.Unknown, TimesheetUpdateFailureCode.Unknown)]
    [InlineData(DataverseFailureCode.Unauthorized, TimesheetUpdateFailureCode.Unknown)]
    [InlineData(DataverseFailureCode.RecordNotFound, TimesheetUpdateFailureCode.NotFound)]
    [InlineData(DataverseFailureCode.PicklistValueOutOfRange, TimesheetUpdateFailureCode.Unknown)]
    [InlineData(DataverseFailureCode.Throttling, TimesheetUpdateFailureCode.Unknown)]
    [InlineData(DataverseFailureCode.SearchableEntityNotFound, TimesheetUpdateFailureCode.Unknown)]
    [InlineData(DataverseFailureCode.DuplicateRecord, TimesheetUpdateFailureCode.Unknown)]
    [InlineData(DataverseFailureCode.InvalidPayload, TimesheetUpdateFailureCode.Unknown)]
    [InlineData(DataverseFailureCode.UserNotEnabled, TimesheetUpdateFailureCode.Unknown)]
    [InlineData(DataverseFailureCode.PrivilegeDenied, TimesheetUpdateFailureCode.Unknown)]
    [InlineData(DataverseFailureCode.InvalidFileSize, TimesheetUpdateFailureCode.Unknown)]
    public static async Task UpdateInvokeAsync_DataverseResultIsFailure_ExpectFailure(
        DataverseFailureCode sourceFailureCode, TimesheetUpdateFailureCode expectedFailureCode)
    {
        var sourceException = new Exception("Some error message");
        var dataverseFailure = sourceException.ToFailure(sourceFailureCode, "Some failure message");

        var mockDataverseApi = BuildMockDataverseApi<ProjectJson>(dataverseFailure, SomeProjectJsonOut);
        var func = new TimesheetModifyFunc(mockDataverseApi.Object);

        var input = new TimesheetUpdateIn(
            timesheetId: Guid.Parse("80108b86-61ae-47ea-bd61-6d0c126a42b4"),
            date: new(2024, 06, 07),
            project: new TimesheetProject(
                id: new("190fd90c-64be-4d6e-8764-44c567b40ef9"),
                type: ProjectType.Project),
            duration: 2,
            description: "Some description");

        var actual = await func.InvokeAsync(input, default);
        var expected = Failure.Create(expectedFailureCode, "Some failure message", sourceException);

        Assert.StrictEqual(expected, actual);
    }

    [Fact]
    public static async Task UpdateInvokeAsync_DataverseResultIsSuccess_ExpectSuccess()
    {
        var mockDataverseApi = BuildMockDataverseApi<IncidentJson>(Result.Success<Unit>(default), SomeIncidentJsonOut);
        var func = new TimesheetModifyFunc(mockDataverseApi.Object);

        var input = new TimesheetUpdateIn(
            timesheetId: Guid.Parse("80108b86-61ae-47ea-bd61-6d0c126a42b4"),
            date: new(2024, 06, 07),
            project: new TimesheetProject(
                id: new("190fd90c-64be-4d6e-8764-44c567b40ef9"),
                type: ProjectType.Incident),
            duration: 2,
            description: "Some description");

        var actual = await func.InvokeAsync(input, default);
        var expected = Result.Success<Unit>(default);

        Assert.StrictEqual(expected, actual);
    }
}
