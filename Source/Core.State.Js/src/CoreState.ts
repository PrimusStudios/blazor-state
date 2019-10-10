import {  JsonRequestHandlerMethodName, JsonRequestHandlerName } from './Constants';

export class CoreState {
  async DispatchRequest(requestTypeFullName: string, request: any ) {
    const requestAsJson = JSON.stringify(request);

    console.log(`Dispatching request of Type ${requestTypeFullName}: ${requestAsJson}`);
    await window[JsonRequestHandlerName].invokeMethodAsync(JsonRequestHandlerMethodName, requestTypeFullName, requestAsJson);
  }
}