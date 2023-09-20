export class UserDetails {
  id: number;
  fullName: string;
  email: string;
  isAdmin: boolean;

  constructor(id: number, fullName: string, email: string, isAdmin: boolean) {
    this.id = id;
    this.fullName = fullName;
    this.email = email;
    this.isAdmin = isAdmin;
  }
}
