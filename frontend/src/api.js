const API_URLS = [
  import.meta.env.VITE_API_URL || 'http://localhost:5281',
  'http://localhost:5000',
].filter((url, index, urls) => urls.indexOf(url) === index);

async function request(path, options = {}) {
  let response;
  let lastError;

  for (const apiUrl of API_URLS) {
    try {
      response = await fetch(`${apiUrl}${path}`, {
        headers: {
          'Content-Type': 'application/json',
          ...options.headers,
        },
        ...options,
      });
      break;
    } catch (error) {
      lastError = error;
    }
  }

  if (!response) {
    throw new Error(
      `Не удалось подключиться к API. Запустите backend на порту 5281 или 5000. (${lastError.message})`,
    );
  }

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || `Ошибка запроса: ${response.status}`);
  }

  return response.status === 204 ? null : response.json();
}

export const organizersApi = {
  getAll: () => request('/api/organizers'),
  create: (userName) =>
    request('/api/organizers', {
      method: 'POST',
      body: JSON.stringify({ userName }),
    }),
  update: (id, userName) =>
    request(`/api/organizers/${id}`, {
      method: 'PUT',
      body: JSON.stringify({ userName }),
    }),
  remove: (id) =>
    request(`/api/organizers/${id}`, {
      method: 'DELETE',
    }),
};
