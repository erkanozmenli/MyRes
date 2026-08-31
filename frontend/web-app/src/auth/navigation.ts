export function redirectToLogin(returnUrl?: string) {
    const url = returnUrl ?? window.location.pathname + window.location.search;
    window.location.href = `/bff/login?returnUrl=${encodeURIComponent(url)}`;
}