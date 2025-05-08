// MIT License

// Copyright(c) 2023 podNET Hungary

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

interface NavigationOptions {
    forceLoad: boolean;
    replaceHistoryEntry: boolean;
    historyEntryState?: string;
}

interface EventTypeOptions {
    browserEventName?: string;
    createEventArgs?: (event: Event) => unknown;
}

interface BlazorEvent {
    type: keyof BlazorEventMap;
}

interface BlazorEventMap {
    'enhancedload': BlazorEvent;
}

declare enum LogLevel {
    Trace = 0,
    Debug = 1,
    Information = 2,
    Warning = 3,
    Error = 4,
    Critical = 5,
    None = 6
}

interface WebStartOptions {
    logLevel?: LogLevel;
}

type ComponentParameters = Map<string, any> | null;
declare interface DynamicRootComponent {
    setParameters(parameters?: ComponentParameters): any;
    dispose(): Promise<void>;
}

interface IBlazorWeb {
    navigateTo(uri: string, options: NavigationOptions): void;
    registerCustomEventType(eventName: string, options: EventTypeOptions): void;
    addEventListener<K extends keyof BlazorEventMap>(type: K, listener: (ev: BlazorEventMap[K]) => void): void;
    removeEventListener<K extends keyof BlazorEventMap>(type: K, listener: (ev: BlazorEventMap[K]) => void): void;
    rootComponents: {
        add(toElement: Element, componentIdentifier: string, initialParameters?: ComponentParameters): Promise<DynamicRootComponent>;
    }
    start<TOptions extends WebStartOptions>(options?: TOptions): Promise<void>;
}

type WebAssemblyBootResourceType = 'assembly' | 'pdb' | 'dotnetjs' | 'dotnetwasm' | 'globalization' | 'manifest' | 'configuration';
type SingleAssetBehaviors = "dotnetwasm" | "js-module-dotnet" | "js-module-threads" | "js-module-runtime" | "js-module-native" | "manifest";
type AssetBehaviors = SingleAssetBehaviors | "resource" | "assembly" | "pdb" | "heap" | "icu" | "vfs" | "js-module-library-initializer" | "symbols";

type BeforeBlazorWebAssemblyStartedCallback = (options: Partial<WebAssemblyStartOptions>) => Promise<void>;
type AfterBlazorWebAssemblyStartedCallback = (blazor: IBlazorWebWebAssembly) => Promise<void>;
type WebAssemblyInitializers = {
    beforeStart: BeforeBlazorWebAssemblyStartedCallback[];
    afterStarted: AfterBlazorWebAssemblyStartedCallback[];
};

interface WebAssemblyStartOptions {
    /**
     * Overrides the built-in boot resource loading mechanism so that boot resources can be fetched
     * from a custom source, such as an external CDN.
     * @param type The type of the resource to be loaded.
     * @param name The name of the resource to be loaded.
     * @param defaultUri The URI from which the framework would fetch the resource by default. The URI may be relative or absolute.
     * @param integrity The integrity string representing the expected content in the response.
     * @param behavior The detailed behavior/type of the resource to be loaded.
     * @returns A URI string or a Response promise to override the loading process, or null/undefined to allow the default loading behavior.
     * When returned string is not qualified with `./` or absolute URL, it will be resolved against document.baseURI.
     */
    loadBootResource(type: WebAssemblyBootResourceType, name: string, defaultUri: string, integrity: string, behavior: AssetBehaviors): string | Promise<Response> | null | undefined;
    /** Override built-in environment setting on start. */
    environment?: string;
    /** Gets the application culture. This is a name specified in the BCP 47 format. See https://tools.ietf.org/html/bcp47 */
    applicationCulture?: string;
    initializers?: WebAssemblyInitializers;
    /** Allows to override .NET runtime configuration. */
    configureRuntime: InternalAPI;
}

interface WebAssemblyWebStartOptions extends WebStartOptions {
    webAssembly?: WebAssemblyStartOptions;
}

interface IBlazorWebWebAssembly extends IBlazorWeb {
    start(options?: WebAssemblyWebStartOptions): Promise<void>;
}

interface ReconnectionOptions {
    maxRetries: number;
    retryIntervalMilliseconds: number;
    dialogId: string;
}

interface ReconnectionHandler {
    onConnectionDown(options: ReconnectionOptions, error?: Error): void;
    onConnectionUp(): void;
}

type BeforeBlazorServerStartedCallback = (options: Partial<CircuitStartOptions>) => Promise<void>;
type AfterBlazorServerStartedCallback = (blazor: IBlazorWebServer) => Promise<void>;
type ServerInitializers = {
    beforeStart: BeforeBlazorServerStartedCallback[];
    afterStarted: AfterBlazorServerStartedCallback[];
};

interface CircuitHandler {
    onCircuitOpened?: () => void;
    onCircuitClosed?: () => void;
}

interface CircuitStartOptions {
    configureSignalR?: (builder: HubConnectionBuilder) => void;
    logLevel?: LogLevel;
    reconnectionOptions?: ReconnectionOptions;
    reconnectionHandler?: ReconnectionHandler;
    initializers?: ServerInitializers;
    circuitHandlers?: CircuitHandler[];
}

interface ServerWebStartOptions extends WebStartOptions {
    circuit?: CircuitStartOptions;
}

interface IBlazorWebServer extends IBlazorWeb {
    start(options?: ServerWebStartOptions): Promise<void>;
    disconnect(): void;
    reconnect(existingConnection?: HubConnection): Promise<boolean>;
    defaultReconnectionHandler: ReconnectionHandler;
}

interface SsrStartOptions {
    /**
     * If true, does not attempt to preserve DOM nodes when performing dynamic updates to SSR content
     * (for example, during enhanced navigation or streaming rendering).
     */
    disableDomPreservation?: boolean;
    /**
     * Configures how long to wait after all Blazor Server components have been removed from the document
     * before closing the circuit.
     */
    circuitInactivityTimeoutMs?: number;
}

interface SsrWebStartOptions extends WebStartOptions {
    ssr?: SsrStartOptions;
}
interface IBlazorWebSsr extends IBlazorWeb {
    start(options?: SsrWebStartOptions): Promise<void>;
}

type InternalAPI = unknown;
interface IBlazorInternal {
    platform?: InternalAPI;
    runtime: InternalAPI;
    _internal: InternalAPI;
}