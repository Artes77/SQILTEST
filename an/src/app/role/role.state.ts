import { Role } from '../shared/model/role.model';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { RoleService } from '../shared/service/role.service'
import { AddRole, DeleteRole, GetRoleById, GetRoles, UpdateRole } from './role.action';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators'
import { throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

export class RoleStateModel {
    roles: Role[];
    role: Role;
}

@State<RoleStateModel>({
    name: 'roles',
    defaults:{
        roles:[],
        role: null
    }
})
export class RoleState {
    constructor(private roleService: RoleService, private toastr:ToastrService) {}

    @Selector()
    static getRoleList(state: RoleStateModel){
        return state.roles;
    }

    @Selector()
    static getSelectedRole(state: RoleStateModel){
        console.log('getSelectedHero!');
        return state.role;
    }

    @Action(GetRoles)
    GetRoles({ getState, setState}: StateContext<RoleStateModel>
    )
    {
        return this.roleService.getAll().pipe(tap(response=>{
            const state = getState();
            setState({
                ...state,
                roles: response
            });
        }),
        catchError((err: HttpErrorResponse) => {
            alert('Something happened. Please try again.');
            return throwError(err.message);
        })
      );
    }

    @Action(GetRoleById)
    getRoleById(
        { getState, setState, patchState }: StateContext<RoleStateModel>,
        { id }: GetRoleById
    )  {
        return this.roleService.get(id).pipe(tap(response => {
            const state=getState();
            patchState({
                ...state,
                role: response
            });
        }),
        catchError((err: HttpErrorResponse)=>{
            alert('Something happened. Place try again.');
            return throwError(err.message);
        })
      );
    }

    @Action(AddRole)
    addPermission(
      { getState, patchState }: StateContext<RoleStateModel>,
      { payload }: AddRole
    ) {
      return this.roleService.create(payload).pipe(
        tap(response => {
          const state = getState();
          patchState({
            roles: [...state.roles, response]
          });
        }),
        catchError((err: HttpErrorResponse) => {
          alert('Something happened. Please try again.');
          return throwError(err.message);
        })
      );
    }

    @Action(UpdateRole)
    updateRole(
      { getState, setState }: StateContext<RoleStateModel>,
      { payload }: UpdateRole
    ) {
      // Optimistic update
      const previousState = getState();
      const state = getState();
      const roles = [...state.roles];
      const index = roles.findIndex(item => item.id === payload.id);
      roles[index] = payload;
      setState({
        ...state,
        roles
      });
      return this.roleService.update(payload).pipe(
        catchError((err: HttpErrorResponse) => {
          alert('Something happened. Please try again.');
          setState({
            ...state,
            roles: previousState.roles
          });
          return throwError(err.message);
        })
      );
    }

    @Action(DeleteRole)
    deleteRole(
      { getState, setState }: StateContext<RoleStateModel>,
      { id }: DeleteRole
    ) {
      // Optimistic update
      const previousState = getState();
      const state = getState();
      const filteredArray = state.roles.filter(h => h.id !== id);
      return this.roleService.delete(id).pipe(
        catchError((err: HttpErrorResponse) => {
          alert('Something happened. Please try again.');
          setState({
            ...state,
            roles: previousState.roles
          });
          return throwError(err.message);
        })
      ).subscribe(res=>{
        if(res){
          setState({
            ...state,
            roles: filteredArray
          });
          this.toastr.success('Success', 'server success');
        }else {
          setState({
            ...state,
            roles: previousState.roles
          });
          this.toastr.error('Error', 'server error');
        }
      });
    }
}
