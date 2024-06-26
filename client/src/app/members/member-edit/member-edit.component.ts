import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { AccountService } from './../../_services/account.service';
import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/_models/user';
import { Member } from 'src/app/_modules/member';
import { MemberService } from 'src/app/_services/member.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm | undefined
  @HostListener('window:beforeunload',['$event']) unloadNotification($event:any){
    if(this.editForm?.dirty){
      $event.returnValue = true;
    }
    
  }
  member: Member | undefined;
  user: User | null = null;

  constructor(private accountService: AccountService, private memberService : MemberService, private toastr: ToastrService){
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => this.user = user
    })
  }

  ngOnInit(): void {    
    this.loadMember()
  }

  loadMember(){
    if(!this.user) return

    this.memberService.getMember(this.user.username).subscribe({
      next: member => this.member = member
    })
  }

  updateMember(){
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: () => {
        this.toastr.success('Profile updated successfully');
        this.editForm?.reset(this.member)
      }
    })
    
  }

}
