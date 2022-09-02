export class EmployeeModel {
  constructor(name: string, id?: number) {
    this.id = id;
    this.name = name;
  }
  id?: number;
  name: string;
}
