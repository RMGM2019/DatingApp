import { Member } from './../../_modules/member';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent  implements OnInit{
  @Input() member :Member | undefined

  ngOnInit(): void {
  }
}
