import { User } from '../shared/model/user.model';

export class GetUsers {
    static readonly type = '[User] Get';
}

export class GetUserById {
    static readonly type = '[User] GetById';
    constructor(public id: number){}
}

export class AddUser {
    static readonly type = '[User] Add';
    constructor(public payload: User){}
}

export class UpdateUser {
    static readonly type = '[User] Update';
    constructor(public payload: User){}
}

export class DeleteUser {
    static readonly type = '[User] Delete';
    constructor(public id: number) {}
}
