import { IBlazorWeb } from "../TypeScript/blazor";
import { katex } from "./lib/katex.js"
export function afterWebStarted(blazor: IBlazorWeb) {
    customElements.define("static-katex-view", class extends HTMLElement {
        static observedAttributes = ["parameters"];
        attributeChangedCallback(name: string, oldValue: string | null, newValue: string | null) {

            if (newValue !== null) {
                const { tex, options } = JSON.parse(newValue);
                this.innerHTML = katex.renderToString(tex, options);
            }
        }
    });

}
