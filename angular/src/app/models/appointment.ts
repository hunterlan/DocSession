export class Appointment {
  id: number;
  time: string;
  fullName: string;

  constructor(id: number, time: string, fullName: string) {
    this.id = id;
    this.time = time;
    this.fullName = fullName;
  }
}
