import { TestBed } from '@angular/core/testing';
import { MembersList } from './members-list';

describe('MembersList', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MembersList],
    }).compileComponents();
  });

  it('should render member names and active class from members input', async () => {
    const fixture = TestBed.createComponent(MembersList);
    fixture.componentRef.setInput('members', [
      { id: 1, name: 'Ada', active: true },
      { id: 2, name: 'Bob', active: false },
    ]);
    await fixture.whenStable();

    const compiled = fixture.nativeElement as HTMLElement;
    const items = compiled.querySelectorAll('[data-testid="member-list"] li');
    expect(items.length).toBe(2);
    expect(items[0]?.textContent?.trim()).toBe('Ada');
    expect(items[1]?.textContent?.trim()).toBe('Bob');
    expect(items[0]?.classList.contains('active')).toBe(true);
    expect(items[1]?.classList.contains('active')).toBe(false);
  });
});
