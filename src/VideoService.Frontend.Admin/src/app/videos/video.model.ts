export class Video { 

    public id:any;
    
    public name:string;

    public static fromJSON(data: { name:string }): Video {

        let video = new Video();

        video.name = data.name;

        return video;
    }
}
