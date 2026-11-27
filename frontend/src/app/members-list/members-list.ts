import { Component, input } from '@angular/core';

export type MemberRow = { id: number; name: string; active: boolean };

@Component({
  selector: 'app-members-list',
  templateUrl: './members-list.html',
})
export class MembersList {
  members = input.required<MemberRow[]>();
}
