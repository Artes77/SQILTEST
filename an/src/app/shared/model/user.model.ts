import { Role } from '../model/role.model'

export interface IUser{
    id?: number;
    firstName?: string;
    lastName?: string;
    roleId?: number;
    role?: Role;
}

export class User implements IUser{
    constructor(
        public id?: number,
        public firstName?: string,
        public lastName?: string,
        public roleId?: number,
        public role?: Role
    ){}
}
