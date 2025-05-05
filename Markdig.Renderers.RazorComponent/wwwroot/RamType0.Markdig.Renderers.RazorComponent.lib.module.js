export function afterWebStarted(blazor) {
    customElements.define("katex-view", class extends HTMLElement {
        static observedAttributes = ["parameters"];
        attributeChangedCallback(name, oldValue, newValue) {
            if (newValue !== null) {
                const { tex, options } = JSON.parse(newValue);
                this.innerHTML = katex.renderToString(tex, options);
            }
        }
    });
}
//# sourceMappingURL=RamType0.Markdig.Renderers.RazorComponent.lib.module.js.map