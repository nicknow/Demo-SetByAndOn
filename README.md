# Microsoft Dataverse Set CreatedBy, CreatedOn, ModifiedOn, and ModifiedBy Columns

**FIRST DRAFT: Updated README Coming Soon**

This is experimental code for demonstration only. It is not intended for production use.

Register as a Pre-Operation plugin on the _create_ message for a standard table.

In your _create_ request set a parameter with the key of **tag**. The value of this parameter should be JSON text containing to values **By** and **On**. **By** must be a valid guid of a _systemuser_ and **On** must be a value date-time value (I recommend ISO 8601 format.) See the example below.

When the plugin is properly registered and a valid JSON payload is in the **tag** parameter the plugin will force the values in the JSON into the _CreatedBy_, _CreatedOn_, _ModifiedOn_, and _ModifiedBy_ columns.

See the code below for an example.


```
var createRequest = new CreateRequest();
createRequest.Parameters.Add("tag", @"{""By"":""{ef4aed4d-eeeb-ec11-bb3d-000d3a37b142}"",""On"":""2022-05-15T18:25:43-05:00""}");
var entity = new Entity("account");
entity["name"] = "Blog Demo # 1";
createRequest.Target = entity;
dataverseService.Execute(createRequest);
```

Copyright (c) 2023 Nicolas A. Nowinski and released under the MIT License.