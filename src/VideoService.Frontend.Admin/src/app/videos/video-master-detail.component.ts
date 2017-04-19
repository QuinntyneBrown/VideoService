import { VideoAdd, VideoDelete, VideoEdit, videoActions } from "./video.actions";
import { Video } from "./video.model";
import { VideoService } from "./video.service";

const template = require("./video-master-detail.component.html");
const styles = require("./video-master-detail.component.scss");

export class VideoMasterDetailComponent extends HTMLElement {
    constructor(
        private _videoService: VideoService = VideoService.Instance	
	) {
        super();
        this.onVideoAdd = this.onVideoAdd.bind(this);
        this.onVideoEdit = this.onVideoEdit.bind(this);
        this.onVideoDelete = this.onVideoDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "videos"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.videos = await this._videoService.get();
        this.videoListElement.setAttribute("videos", JSON.stringify(this.videos));
    }

    private _setEventListeners() {
        this.addEventListener(videoActions.ADD, this.onVideoAdd);
        this.addEventListener(videoActions.EDIT, this.onVideoEdit);
        this.addEventListener(videoActions.DELETE, this.onVideoDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(videoActions.ADD, this.onVideoAdd);
        this.removeEventListener(videoActions.EDIT, this.onVideoEdit);
        this.removeEventListener(videoActions.DELETE, this.onVideoDelete);
    }

    public async onVideoAdd(e) {

        await this._videoService.add(e.detail.video);
        this.videos = await this._videoService.get();
        
        this.videoListElement.setAttribute("videos", JSON.stringify(this.videos));
        this.videoEditElement.setAttribute("video", JSON.stringify(new Video()));
    }

    public onVideoEdit(e) {
        this.videoEditElement.setAttribute("video", JSON.stringify(e.detail.video));
    }

    public async onVideoDelete(e) {

        await this._videoService.remove(e.detail.video.id);
        this.videos = await this._videoService.get();
        
        this.videoListElement.setAttribute("videos", JSON.stringify(this.videos));
        this.videoEditElement.setAttribute("video", JSON.stringify(new Video()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "videos":
                this.videos = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Video> { return this.videos; }

    private videos: Array<Video> = [];
    public video: Video = <Video>{};
    public get videoEditElement(): HTMLElement { return this.querySelector("ce-video-edit-embed") as HTMLElement; }
    public get videoListElement(): HTMLElement { return this.querySelector("ce-video-list-embed") as HTMLElement; }
}

customElements.define(`ce-video-master-detail`,VideoMasterDetailComponent);
