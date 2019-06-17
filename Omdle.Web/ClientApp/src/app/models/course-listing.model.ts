import { Course } from "./course.model";

export class CourseListing {
  constructor(
    public totalCount: number,
    public courses: Course[]) {
  }
}

