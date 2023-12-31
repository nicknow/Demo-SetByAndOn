﻿namespace ThinkCrm.Core.PluginCore.Helper
{
    public enum PipelineStage
    {
        Prevalidation = 10,
        Preoperation = 20,
        MainOperation = 30,
        Postoperation = 40
    }

    public enum SupportedDeployment
    {
        ServerOnly = 0,
        MicrosoftDynamicsCrmClientForOutlookOnly = 1,
        Both = 2
    }

    public enum ExecutionMode
    {
        Synchronous = 0,
        Asynchronous = 1
    }

    public enum IsolationMode
    {
        None = 1,
        Sandbox = 2
    }

    public enum MessageType
    {
        UnknownReferToContextMessageString
        ,
        None
        ,
        AddItem
            ,
        AddListMembers
            ,
        AddMember
            ,
        AddMembers
            ,
        AddPrivileges
            ,
        AddProductToKit
            ,
        AddRecurrence
            ,
        AddToQueue
            ,
        Assign
            ,
        AssignUserRoles
            ,
        Associate
            ,
        BackgroundSend
            ,
        Book
            ,
        Cancel
            ,
        CheckIncoming
            ,
        CheckPromote
            ,
        Clone
            ,
        Close
            ,
        CopyDynamicListToStatic
            ,
        CopySystemForm
            ,
        Create
            ,
        CreateException
            ,
        CreateInstance
            ,
        Delete
            ,
        DeleteOpenInstances
            ,
        DeliverIncoming
            ,
        DeliverPromote
            ,
        DetachFromQueue
            ,
        Disassociate
            ,
        Execute
            ,
        ExecuteById
            ,
        Export
            ,
        ExportAll
            ,
        ExportCompressed
            ,
        ExportCompressedAll
            ,
        GrantAccess
            ,
        Handle
            ,
        Import
            ,
        ImportAll
            ,
        ImportCompressedAll
            ,
        ImportCompressedWithProgress
            ,
        ImportWithProgress
            ,
        LockInvoicePricing
            ,
        LockSalesOrderPricing
            ,
        Lose
            ,
        Merge
            ,
        ModifyAccess
            ,
        Publish
            ,
        PublishAll
            ,
        QualifyLead
            ,
        Recalculate
            ,
        RemoveItem
            ,
        RemoveMember
            ,
        RemoveMembers
            ,
        RemovePrivilege
            ,
        RemoveProductFromKit
            ,
        RemoveRelated
            ,
        RemoveUserRoles
            ,
        ReplacePrivileges
            ,
        Reschedule
            ,
        Retrieve
            ,
        RetrieveExchangeRate
            ,
        RetrieveFilteredForms
            ,
        RetrieveMultiple
            ,
        RetrievePersonalWall
            ,
        RetrievePrincipalAccess
            ,
        RetrieveRecordWall
            ,
        RetrieveSharedPrincipalsAndAccess
            ,
        RetrieveUnpublished
            ,
        RetrieveUnpublishedMultiple
            ,
        RevokeAccess
            ,
        Route
            ,
        Send
            ,
        SendFromTemplate
            ,
        SetRelated
            ,
        SetState
            ,
        SetStateDynamicEntity
            ,
        TriggerServiceEndpointCheck
            ,
        UnlockInvoicePricing
            ,
        UnlockSalesOrderPricing
            ,
        Update
            ,
        ValidateRecurrenceRule
            , Win
    }
}
