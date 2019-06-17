export class RegisterRequest {
  constructor(
    public UserName: string,
    public Firstname: string,
    public Lastname: string, 
    public Password: string,
    public Email: string,


  ) {
  }
}
