import { Role } from '../shared/model/role.model';

export class GetRoles {
    static readonly type = '[Role] Get';
}

export class GetRoleById {
    static readonly type = '[Role] GetById';
    constructor(public id: number){}
}

export class AddRole {
    static readonly type = '[Role] Add';
    constructor(public payload: Role){}
}

export class UpdateRole {
    static readonly type = '[Role] Update';
    constructor(public payload: Role){}
}

export class DeleteRole {
    static readonly type = '[Role] Delete';
    constructor(public id: number) {}
}
