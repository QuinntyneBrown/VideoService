import { Video } from "./video.model";

const template = require("./video-list-embed.component.html");
const styles = require("./video-list-embed.component.scss");

export class VideoListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "videos"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.videos.length; i++) {
            let el = this._document.createElement(`ce-video-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.videos[i]));
            this.appendChild(el);
        }    
    }

    videos:Array<Video> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "videos":
                this.videos = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-video-list-embed", VideoListEmbedComponent);
