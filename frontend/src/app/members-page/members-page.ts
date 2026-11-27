import { Component, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthSession } from '../auth-session';
import { MembersApi } from '../members-api';
import { MemberRow, MembersList } from '../members-list/members-list';

@Component({
  selector: 'app-members-page',
  imports: [FormsModule, MembersList],
  templateUrl: './members-page.html',
})
export class MembersPage {
  private readonly session = inject(AuthSession);
  private readonly membersApi = inject(MembersApi);

  protected readonly members = signal<MemberRow[]>([]);
  protected readonly search = signal('');
  protected readonly filteredMembers = computed(() => {
    const q = this.search().toLowerCase();
    return this.members().filter((m) => m.name.toLowerCase().includes(q));
  });
  protected readonly memberCount = computed(() => this.members().length);

  constructor() {
    this.load();
  }

  load(): void {
    const token = this.session.token();
    if (!token) {
      return;
    }
    this.membersApi.list(token).subscribe((list) => {
      this.members.set(list.map((m) => ({ id: m.id, name: m.name, active: true })));
    });
  }
}
