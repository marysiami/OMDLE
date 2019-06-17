export class Course {
  constructor(
    public title: string,
    public id: string,
    public ownerName: string,
    public ownerSurname: string,
    public lessonsCount: number) {
  }
}
