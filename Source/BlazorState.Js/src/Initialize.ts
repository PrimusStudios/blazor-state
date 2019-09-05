import { BlazorState } from './BlazorState';
import { ReduxDevTools} from './ReduxDevTools';
import {
  BlazorStateName,
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
  console.log("Initialize BlazorState");
  if (typeof window !== 'undefined' && !window[BlazorStateName]) {
    window[BlazorStateName] = new BlazorState();
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