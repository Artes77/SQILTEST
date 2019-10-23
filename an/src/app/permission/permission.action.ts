import { Permission } from '../shared/model/permission.model';

export class GetPermissions {
    static readonly type = '[Permission] Get';
}

export class GetPermissionById {
    static readonly type = '[Permission] GetById';
    constructor(public id: number){}
}

export class AddPermission {
    static readonly type = '[Permission] Add';
    constructor(public payload: Permission){}
}

export class UpdatePermission {
    static readonly type = '[Permission] Update';
    constructor(public payload: Permission){}
}

export class DeletePermission {
    static readonly type = '[Permission] Delete';
    constructor(public id: number) {}
}
