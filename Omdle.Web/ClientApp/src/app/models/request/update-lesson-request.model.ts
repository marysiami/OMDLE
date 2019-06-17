export class UpdateLessonRequest {
  constructor(
    public title: string,
    public content: string,
    public date: string,
    public courseId: string) {
  }
}

