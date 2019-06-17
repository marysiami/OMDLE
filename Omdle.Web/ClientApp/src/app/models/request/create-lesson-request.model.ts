export class CreateLessonRequest {
  constructor(
    public title: string,
    public content: string,
    public date:string,
    public courseId:string
  ) {
  }
}
