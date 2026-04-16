export function formatAppointmentDate(dateValue: string): string {
  return new Intl.DateTimeFormat('en-IN', {
    weekday: 'long',
    day: 'numeric',
    month: 'long',
    year: 'numeric'
  }).format(new Date(`${dateValue}T00:00:00`));
}

export function formatAppointmentTime(timeValue: string): string {
  return new Intl.DateTimeFormat('en-IN', {
    hour: 'numeric',
    minute: '2-digit',
    hour12: true
  }).format(new Date(`1970-01-01T${timeValue}`));
}

export function formatAppointmentTimeRange(startTime: string, endTime: string): string {
  return `${formatAppointmentTime(startTime)} - ${formatAppointmentTime(endTime)}`;
}
