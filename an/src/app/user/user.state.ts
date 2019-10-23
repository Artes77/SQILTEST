import { User } from '../shared/model/user.model';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { UserService } from '../shared/service/user.service'
import { AddUser, DeleteUser, GetUserById, GetUsers, UpdateUser } from './user.action';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators'
import { throwError } from 'rxjs';
import { StateContextFactory } from '@ngxs/store/src/internal/state-context-factory';

export class UserStateModel {
    users: User[];
    user: User;
}

@State<UserStateModel>({
    name: 'users',
    defaults:{
        users:[],
        user: null
    }
})
export class UserState {
    constructor(private userService: UserService) {}

    @Selector()
    static getUserList(state: UserStateModel){
        return state.users;
    }

    @Selector()
    static getSelectedUser(state: UserStateModel){
        console.log('getSelectedHero!');
        return state.user;
    }

    @Action(GetUsers)
    GetUsers({ getState, setState}: StateContext<UserStateModel>){
        return this.userService.getAll().pipe(tap(response=>{
            const state = getState();
            setState({
                ...state,
                users: response
            });
        }),
        catchError((err: HttpErrorResponse) => {
            alert('Something happened. Please try again.');
            return throwError(err.message);
        })
      );
    }

    @Action(GetUserById)
    getUserById(
        { getState, setState, patchState }: StateContext<UserStateModel>,
        { id }: GetUserById
    )  {
        return this.userService.get(id).pipe(tap(response => {
            const state=getState();
            patchState({
                ...state,
                user: response
            });
        }),
        catchError((err: HttpErrorResponse)=>{
            alert('Something happened. Place try again.');
            return throwError(err.message);
        })
      );
    }

    @Action(AddUser)
    addUser(
      { getState, patchState }: StateContext<UserStateModel>,
      { payload }: AddUser
    ) {
      return this.userService.create(payload).pipe(
        tap(response => {
          const state = getState();
          patchState({
            users: [...state.users, response]
          });
        }),
        catchError((err: HttpErrorResponse) => {
          alert('Something happened. Please try again.');
          return throwError(err.message);
        })
      );
    }

    @Action(UpdateUser)
    updateUser(
      { getState, setState }: StateContext<UserStateModel>,
      { payload }: UpdateUser
    ) {
      // Optimistic update
      const previousState = getState();
      const state = getState();
      const users = [...state.users];
      const index = users.findIndex(item => item.id === payload.id);
      users[index] = payload;
      setState({
        ...state,
        users
      });
      return this.userService.update(payload).pipe(
        catchError((err: HttpErrorResponse) => {
          alert('Something happened. Please try again.');
          setState({
            ...state,
            users: previousState.users
          });
          return throwError(err.message);
        })
      );
    }

    @Action(DeleteUser)
    deletePermission(
      { getState, setState }: StateContext<UserStateModel>,
      { id }: DeleteUser
    ) {
      // Optimistic update
      const previousState = getState();
      const state = getState();
      const filteredArray = state.users.filter(h => h.id !== id);
      setState({
        ...state,
        users: filteredArray
      });
      return this.userService.delete(id).pipe(
        catchError((err: HttpErrorResponse) => {
          alert('Something happened. Please try again.');
          setState({
            ...state,
            users: previousState.users
          });
          return throwError(err.message);
        })
      );
    }
}
