import { Video } from "./video.model";
import { EditorComponent } from "../shared";
import {  VideoDelete, VideoEdit, VideoAdd } from "./video.actions";

const template = require("./video-edit-embed.component.html");
const styles = require("./video-edit-embed.component.scss");

export class VideoEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "video",
            "video-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.video ? "Edit Video": "Create Video";

        if (this.video) {                
            this._nameInputElement.value = this.video.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._createButtonElement.addEventListener("click", this.onCreate);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._createButtonElement.removeEventListener("click", this.onCreate);
    }

    public onSave() {
        const video = {
            id: this.video != null ? this.video.id : null,
            name: this._nameInputElement.value
        } as Video;
        
        this.dispatchEvent(new VideoAdd(video));            
    }

    public onCreate() {        
        this.dispatchEvent(new VideoEdit(new Video()));            
    }

    public onDelete() {        
        const video = {
            id: this.video != null ? this.video.id : null,
            name: this._nameInputElement.value
        } as Video;

        this.dispatchEvent(new VideoDelete(video));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "video-id":
                this.videoId = newValue;
                break;
            case "video":
                this.video = JSON.parse(newValue);
                if (this.parentNode) {
                    this.videoId = this.video.id;
                    this._nameInputElement.value = this.video.name != undefined ? this.video.name : "";
                    this._titleElement.textContent = this.videoId ? "Edit Video" : "Create Video";
                }
                break;
        }           
    }

    public videoId: any;
    
	public video: Video;
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".video-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
	private get _nameInputElement(): HTMLInputElement { return this.querySelector(".video-name") as HTMLInputElement;}
}

customElements.define(`ce-video-edit-embed`,VideoEditEmbedComponent);
