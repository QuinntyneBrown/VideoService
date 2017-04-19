import { Video } from "./video.model";

export const videoActions = {
    ADD: "[Video] Add",
    EDIT: "[Video] Edit",
    DELETE: "[Video] Delete",
    VIDEOS_CHANGED: "[Video] Videos Changed"
};

export class VideoEvent extends CustomEvent {
    constructor(eventName:string, video: Video) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { video }
        });
    }
}

export class VideoAdd extends VideoEvent {
    constructor(video: Video) {
        super(videoActions.ADD, video);        
    }
}

export class VideoEdit extends VideoEvent {
    constructor(video: Video) {
        super(videoActions.EDIT, video);
    }
}

export class VideoDelete extends VideoEvent {
    constructor(video: Video) {
        super(videoActions.DELETE, video);
    }
}

export class VideosChanged extends CustomEvent {
    constructor(videos: Array<Video>) {
        super(videoActions.VIDEOS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { videos }
        });
    }
}
