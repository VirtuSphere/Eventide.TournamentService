import { useEffect, useMemo, useState } from 'react';
import {
  AlertCircle,
  Check,
  ChevronRight,
  CircleUserRound,
  Command,
  LoaderCircle,
  Pencil,
  Plus,
  RefreshCw,
  Search,
  ShieldCheck,
  Sparkles,
  Trash2,
  X,
} from 'lucide-react';
import { organizersApi } from './api';

function App() {
  const [organizers, setOrganizers] = useState([]);
  const [search, setSearch] = useState('');
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');
  const [notice, setNotice] = useState('');
  const [editing, setEditing] = useState(null);
  const [isFormOpen, setIsFormOpen] = useState(false);

  const loadOrganizers = async () => {
    setIsLoading(true);
    setError('');
    try {
      setOrganizers(await organizersApi.getAll());
    } catch (requestError) {
      setError(requestError.message);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    loadOrganizers();
  }, []);

  const filteredOrganizers = useMemo(() => {
    const query = search.trim().toLowerCase();
    if (!query) return organizers;
    return organizers.filter(
      ({ userName, id }) =>
        userName.toLowerCase().includes(query) || id.toLowerCase().includes(query),
    );
  }, [organizers, search]);

  const openCreate = () => {
    setEditing(null);
    setIsFormOpen(true);
  };

  const openEdit = (organizer) => {
    setEditing(organizer);
    setIsFormOpen(true);
  };

  const handleSaved = (organizer) => {
    setOrganizers((current) =>
      editing
        ? current.map((item) => (item.id === organizer.id ? organizer : item))
        : [organizer, ...current],
    );
    setIsFormOpen(false);
    setEditing(null);
    setNotice(editing ? 'Данные организатора обновлены' : 'Организатор добавлен');
    window.setTimeout(() => setNotice(''), 3500);
  };

  const handleDelete = async (organizer) => {
    if (!window.confirm(`Удалить организатора «${organizer.userName}»?`)) return;
    setError('');
    try {
      await organizersApi.remove(organizer.id);
      setOrganizers((current) => current.filter((item) => item.id !== organizer.id));
      setNotice('Организатор удалён');
      window.setTimeout(() => setNotice(''), 3500);
    } catch (requestError) {
      setError(requestError.message);
    }
  };

  return (
    <div className="app-shell">
      <aside className="sidebar">
        <div className="brand">
          <div className="brand-mark"><Sparkles size={18} /></div>
          <span>eventide</span>
        </div>
        <div className="workspace-label">РАЗДЕЛЫ</div>
        <nav>
          <a className="nav-item active" href="#organizers">
            <ShieldCheck size={18} />
            Организаторы
            <ChevronRight className="nav-chevron" size={16} />
          </a>
          <a className="nav-item disabled" href="#tournaments">
            <Command size={18} />
            Турниры
            <span className="soon">скоро</span>
          </a>
        </nav>
        <div className="sidebar-footer">
          <div className="status-dot" />
          <div>
            <strong>Сервис онлайн</strong>
            <span>API подключён</span>
          </div>
        </div>
      </aside>

      <main className="main-content" id="organizers">
        <header className="topbar">
          <div className="breadcrumbs"><span>Eventide</span><ChevronRight size={14} /><b>Организаторы</b></div>
          <div className="profile"><div className="avatar">A</div><span>Администратор</span></div>
        </header>

        <section className="content">
          <div className="page-heading">
            <div>
              <p className="eyebrow">УПРАВЛЕНИЕ ДОСТУПОМ</p>
              <h1>Организаторы</h1>
              <p className="subtitle">Управляйте пользователями, которые создают и проводят турниры.</p>
            </div>
            <button className="primary-button" onClick={openCreate}><Plus size={18} /> Добавить организатора</button>
          </div>

          <div className="stats-row">
            <div className="stat-card"><span>Всего организаторов</span><strong>{organizers.length}</strong><CircleUserRound size={25} /></div>
            <div className="stat-card"><span>Статус сервиса</span><strong className="online">Работает</strong><div className="pulse" /></div>
          </div>

          <div className="panel">
            <div className="panel-toolbar">
              <div><h2>Список организаторов</h2><span className="count-badge">{filteredOrganizers.length}</span></div>
              <div className="toolbar-actions">
                <label className="search-box"><Search size={17} /><input value={search} onChange={(event) => setSearch(event.target.value)} placeholder="Поиск по имени или ID" /></label>
                <button className="icon-button" title="Обновить" onClick={loadOrganizers}><RefreshCw size={17} /></button>
              </div>
            </div>
            {error && <div className="alert error"><AlertCircle size={18} /><span>{error}</span><button onClick={() => setError('')}><X size={16} /></button></div>}
            {notice && <div className="alert success"><Check size={18} /><span>{notice}</span></div>}
            <div className="table-wrap">
              <table>
                <thead><tr><th>Организатор</th><th>ID</th><th>Статус</th><th aria-label="Действия" /></tr></thead>
                <tbody>
                  {isLoading ? <tr><td className="empty-state" colSpan="4"><LoaderCircle className="spinner" size={24} />Загрузка организаторов...</td></tr>
                    : filteredOrganizers.length === 0 ? <tr><td className="empty-state" colSpan="4"><CircleUserRound size={30} /><strong>{search ? 'Ничего не найдено' : 'Организаторов пока нет'}</strong><span>{search ? 'Попробуйте изменить поисковый запрос.' : 'Добавьте первого организатора, чтобы начать.'}</span></td></tr>
                      : filteredOrganizers.map((organizer) => <tr key={organizer.id}>
                        <td><div className="organizer-cell"><div className="organizer-avatar">{organizer.userName.charAt(0).toUpperCase()}</div><strong>{organizer.userName}</strong></div></td>
                        <td><code>{organizer.id}</code></td>
                        <td><span className="status-badge"><span /> Активен</span></td>
                        <td><div className="row-actions"><button title="Редактировать" onClick={() => openEdit(organizer)}><Pencil size={16} /></button><button className="danger" title="Удалить" onClick={() => handleDelete(organizer)}><Trash2 size={16} /></button></div></td>
                      </tr>)}
                </tbody>
              </table>
            </div>
          </div>
        </section>
      </main>
      {isFormOpen && <OrganizerForm organizer={editing} onClose={() => setIsFormOpen(false)} onSaved={handleSaved} />}
    </div>
  );
}

function OrganizerForm({ organizer, onClose, onSaved }) {
  const [userName, setUserName] = useState(organizer?.userName || '');
  const [isSaving, setIsSaving] = useState(false);
  const [error, setError] = useState('');

  const submit = async (event) => {
    event.preventDefault();
    const value = userName.trim();
    if (!value) {
      setError('Введите имя пользователя');
      return;
    }
    setIsSaving(true);
    setError('');
    try {
      const saved = organizer
        ? await organizersApi.update(organizer.id, value).then(() => ({ ...organizer, userName: value }))
        : await organizersApi.create(value);
      onSaved(saved);
    } catch (requestError) {
      setError(requestError.message);
    } finally {
      setIsSaving(false);
    }
  };

  return <div className="modal-backdrop" onMouseDown={(event) => event.target === event.currentTarget && onClose()}>
    <form className="modal" onSubmit={submit}>
      <div className="modal-header"><div><p className="eyebrow">{organizer ? 'РЕДАКТИРОВАНИЕ' : 'НОВЫЙ ДОСТУП'}</p><h2>{organizer ? 'Изменить организатора' : 'Добавить организатора'}</h2></div><button type="button" className="close-button" onClick={onClose}><X size={19} /></button></div>
      <label className="field-label" htmlFor="userName">Имя пользователя<input id="userName" autoFocus value={userName} onChange={(event) => setUserName(event.target.value)} placeholder="например, organizer-1" /></label>
      {error && <div className="form-error"><AlertCircle size={16} />{error}</div>}
      <div className="modal-actions"><button type="button" className="secondary-button" onClick={onClose}>Отмена</button><button className="primary-button" disabled={isSaving}>{isSaving && <LoaderCircle className="spinner" size={17} />}{organizer ? 'Сохранить' : 'Добавить'}</button></div>
    </form>
  </div>;
}

export default App;
