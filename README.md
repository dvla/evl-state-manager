# EVL State Manager
---

## What it is
A tool to provide state management for .NET MVC applications in a manner similar to Web Forms "ViewState" functionality whilst preventing the client from deciphering or tampering with the data.

The tool uses .NET's [MachineKey API](https://msdn.microsoft.com/en-us/library/system.web.security.machinekey(v=vs.110).aspx) to perform it's Encode and Decode operations. If you use your application in a multi server envrionment, you'll need to ensure each server [shares the same MachineKey config](https://msdn.microsoft.com/en-us/library/ff649308.aspx#paght000007_webfarmdeploymentconsiderations).

## How to use it
* Add a reference to `EVL.Web.Mvc.Shared.StateManager`
* In your controller, call `GetEncryptedViewState(obj)` passing the item you want to store on the client
* Send this string back to the client and store in a hidden field
* In your second controller, call `DecryptFromViewState<t>(string)` passing the type of item you want to decrypt and the encrpytred string as returned by the client
* Check out the *Sample.Web* application to see this in action
