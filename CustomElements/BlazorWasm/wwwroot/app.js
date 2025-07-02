class BlazorWasm {
    #baseURI;

    constructor(sourceURL) {
        this.#baseURI = `${sourceURL.protocol}//${sourceURL.host}`;
    }

    start() {
        const blazorScript = document.createElement("script");
        blazorScript.setAttribute("autostart", "false");

        const base = document.getElementsByTagName("base")[0];
        const baseHref = base.getAttribute("href");
        base.setAttribute("href", this.#baseURI);

        return new Promise((resolve, reject) => {
            blazorScript.onload = () => {
                Blazor.start()
                    .then(resolve)
                    .catch(reject)
                    .finally(() => base.setAttribute("href", baseHref));
            };
            blazorScript.src = `${this.#baseURI}/_framework/blazor.webassembly.js`;
            document.head.appendChild(blazorScript);
        });
    }
}

var blazorWasm = new BlazorWasm(new URL(document.currentScript.src));

const autostart = document.currentScript.getAttribute("autostart");
if (autostart != "false" && autostart != false) {
    blazorWasm.start();
}