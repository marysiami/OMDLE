import { User } from "./user.model";

export class UserListing {
  constructor(
    public totalCount: number,
    public users: User[]) {
  }
}

