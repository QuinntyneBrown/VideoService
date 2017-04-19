import { fetch } from "../utilities";
import { Video } from "./video.model";

export class VideoService {
    constructor(private _fetch = fetch) { }

    private static _instance: VideoService;

    public static get Instance() {
        this._instance = this._instance || new VideoService();
        return this._instance;
    }

    public get(): Promise<Array<Video>> {
        return this._fetch({ url: "/api/video/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { videos: Array<Video> }).videos;
        });
    }

    public getById(id): Promise<Video> {
        return this._fetch({ url: `/api/video/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { video: Video }).video;
        });
    }

    public add(video) {
        return this._fetch({ url: `/api/video/add`, method: "POST", data: { video }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/video/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
