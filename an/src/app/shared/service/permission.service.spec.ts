import { TestBed, async, inject } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { PermissionService } from './permission.service';

describe('PermissionService', () => {
    beforeEach(async()=>{
        TestBed.configureTestingModule({
            imports: [HttpClientModule, HttpClientTestingModule],
            providers: [PermissionService]
        });
    });

    it('should getAll', inject([PermissionService, HttpTestingController], (service:PermissionService, backend: HttpTestingController)=>{
        const mockPermissions = [{id: 1, name: 'abracadabra'}];
        
        service.getAll().subscribe( permissions => {
            expect(permissions).toEqual(mockPermissions);
        });

        backend.expectOne({
            method: 'GET',
            url: service.BaseURL         
        }).flush(mockPermissions)
    }));

    it('should get', inject([PermissionService, HttpTestingController], (service:PermissionService, backend: HttpTestingController)=>{
        const mockPermission = {id: 1, name: 'abracadabra'};
        
        service.get(1).subscribe( permission => {
            expect(permission).toEqual(mockPermission);
        });

        backend.expectOne({
            method: 'GET',
            url: service.BaseURL+'/1'         
        }).flush(mockPermission)
    }));

    it('should be created', inject([PermissionService, HttpTestingController], (service:PermissionService, backend: HttpTestingController)=>{
        expect(service).toBeTruthy();
    }));

    it('should be created', inject([PermissionService, HttpTestingController], (service:PermissionService, backend: HttpTestingController)=>{
        expect(service).toBeTruthy();
    }));
    
    it('should be created', inject([PermissionService, HttpTestingController], (service:PermissionService, backend: HttpTestingController)=>{
        expect(service).toBeTruthy();
    }));
})