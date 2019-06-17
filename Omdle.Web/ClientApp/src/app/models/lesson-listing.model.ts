import { Lesson } from "./lesson.model";


export class LessonListing {
  constructor(
    public totalCount: number,
    public lessons: Lesson[],
    public courseName:string) {
  }
}

