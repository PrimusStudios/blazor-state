import { CoreState } from './CoreState';
import { ReduxDevTools} from './ReduxDevTools';
import {
  CoreStateName,
  InitializeJavaScriptInteropName,
  JsonRequestHandlerName,
  ReduxDevToolsFactoryName,
  ReduxDevToolsName,
} from './Constants';

function InitializeJavaScriptInterop(JsonRequestHandler) {
  console.log("InitializeJavaScriptInterop");
  window[JsonRequestHandlerName] = JsonRequestHandler;
};


function Initialize() {
  console.log("Initialize CoreState");
  if (typeof window !== 'undefined' && !window[CoreStateName]) {
    window[CoreStateName] = new CoreState();
    window[InitializeJavaScriptInteropName] = InitializeJavaScriptInterop;
    window[ReduxDevToolsFactoryName] = ReduxDevToolsFactory;
  }
}

function ReduxDevToolsFactory(): boolean {
  const reduxDevTools = new ReduxDevTools();
  window[ReduxDevToolsName] = reduxDevTools;
  return reduxDevTools.IsEnabled;
}

Initialize();